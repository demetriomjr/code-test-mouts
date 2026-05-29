using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository implementation for sale order operations
/// </summary>
public class SaleOrderRepository : ISaleOrderRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleOrderRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleOrderRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a paginated list of sale orders with their items
    /// </summary>
    /// <param name="page">The page number to retrieve (1-based)</param>
    /// <param name="amountPerPage">The number of orders per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of pages and the list of orders</returns>
    public async Task<(int totalPages, IEnumerable<SaleOrder> orders)> GetOrders(
        int page,
        int amountPerPage,
        bool includeProductList = false,
        int? orderNumberFrom = null,
        int? orderNumberTo = null,
        string? customerName = null,
        string? branchName = null,
        CancelStatus? cancelStatus = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<SaleOrder> query = _context.SaleOrders.AsNoTracking();

        if (includeProductList)
            query = query.Include(x => x.Products);

        if (orderNumberFrom.HasValue)
            query = query.Where(x => x.OrderNumber >= orderNumberFrom.Value);

        if (orderNumberTo.HasValue)
            query = query.Where(x => x.OrderNumber <= orderNumberTo.Value);

        if (!string.IsNullOrWhiteSpace(customerName))
            query = query.Where(x => EF.Functions.ILike(x.CustomerName, $"%{customerName.Trim()}%"));

        if (!string.IsNullOrWhiteSpace(branchName))
            query = query.Where(x => EF.Functions.ILike(x.BranchName, $"%{branchName.Trim()}%"));

        if (cancelStatus.HasValue)
            query = query.Where(x => x.CancelStatus == cancelStatus.Value);

        if (dateFrom.HasValue)
            query = query.Where(x => x.Date >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(x => x.Date <= dateTo.Value);

        var (orderByValue, isDesc) = ParseOrderBy(orderBy);

        query = orderByValue switch
        {
            "ordernumber" => isDesc ? query.OrderByDescending(x => x.OrderNumber) : query.OrderBy(x => x.OrderNumber),
            "customername" => isDesc ? query.OrderByDescending(x => x.CustomerName) : query.OrderBy(x => x.CustomerName),
            "branchname" => isDesc ? query.OrderByDescending(x => x.BranchName) : query.OrderBy(x => x.BranchName),
            "createdat" => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
            "totalsale" => isDesc ? query.OrderByDescending(x => x.TotalSale) : query.OrderBy(x => x.TotalSale),
            _ => isDesc ? query.OrderByDescending(x => x.Date).ThenByDescending(x => x.CreatedAt) : query.OrderBy(x => x.Date).ThenBy(x => x.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)amountPerPage);

        var result = await query.Skip((page - 1) * amountPerPage)
            .Take(amountPerPage)
            .ToListAsync(cancellationToken);

        if (!includeProductList)
        {
            foreach (var order in result)
                order.Products = [];
        }

        return (totalPages, result);
    }

    private static (string orderByField, bool isDesc) ParseOrderBy(string? orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return ("date", false);

        var raw = orderBy.Trim().ToLowerInvariant();
        var tokens = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length == 2)
        {
            var desc = tokens[1] == "desc";
            return (tokens[0], desc);
        }

        if (tokens.Length == 1)
        {
            var token = tokens[0];
            var fields = new[] { "ordernumber", "date", "customername", "branchname", "createdat", "totalsale" };

            foreach (var field in fields)
            {
                if (token == $"{field}desc")
                    return (field, true);

                if (token == $"{field}asc" || token == $"{field}ascii")
                    return (field, false);
            }

            return (token, false);
        }

        return ("date", false);
    }

    /// <summary>
    /// Retrieves a sale order by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale order if found, otherwise null</returns>
    public async Task<SaleOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        return result;
    }

    /// <summary>
    /// Creates a new sale order in the database
    /// </summary>
    /// <param name="order">The sale order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order</returns>
    public async Task<SaleOrder> CreateAsync(order order, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<SaleOrder> UpdateAsync(order order, CancellationToken cancellationToken = default)
    {
        _context.SaleOrders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    /// <summary>
    /// Gets the last valid order number persisted
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The last persisted order number, or 0 if none exist</returns>
    [Obsolete("Deprecated: OrderNumber is generated by database sequence.")]
    public async Task<int> GetLastOrderNumber(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.OrderByDescending(x => x.OrderNumber)
                                              .FirstOrDefaultAsync(cancellationToken);

        if(result is null)
            return 0;

        return result.OrderNumber;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        if (order == null)
            return false;
        
        //This version will ONLY cancel the order if the desired behaviour is so
        //order.CancelStatus = CancelStatus.Cancelled;
        
        //This version will delete the record from database IF it's the desired behaviour
        _context.SaleOrders.Remove(order);
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
