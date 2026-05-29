using Ambev.DeveloperEvaluation.Domain.Entities;
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
    public async Task<(int totalPages, IEnumerable<SaleOrder> orders)> GetOrders(int page, int amountPerPage, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.Include(x => x.Products)
                                              .Skip((page - 1) * amountPerPage).Take(amountPerPage).ToListAsync(cancellationToken);
        var totalPages = await _context.SaleOrders.CountAsync(cancellationToken) / amountPerPage;
        return (totalPages, result);
    }

    /// <summary>
    /// Retrieves a sale order by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale order if found, otherwise null</returns>
    public async Task<SaleOrder> GetOrderById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        return result;
    }

    /// <summary>
    /// Creates a new sale order in the database
    /// </summary>
    /// <param name="order">The sale order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order</returns>
    public async Task<SaleOrder> CreateAsync(SaleOrder order, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Gets the last valid order number persisted
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The last persisted order number, or 0 if none exist</returns>
    public async Task<int> GetLastOrderNumber(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.OrderByDescending(x => x.OrderNumber)
                                              .FirstOrDefaultAsync(cancellationToken);

        if(result is null)
            return 0;

        return result.OrderNumber;
    }

    /// <summary>
    /// Cancels a sale order by setting its IsCancelled flag
    /// </summary>
    /// <param name="orderId">The unique identifier of the order to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the order was cancelled, false if not found</returns>
    public async Task<bool> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(orderId), cancellationToken);

        if(order is null)
            return false;

        order.IsCancelled = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <summary>
    /// Cancels a specific item within a sale order
    /// </summary>
    /// <param name="orderId">The unique identifier of the order</param>
    /// <param name="itemId">The unique identifier of the item to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the item was cancelled, false if the order or item was not found</returns>
    public async Task<bool> CancelOrderItemAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var order = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(orderId), cancellationToken);

        if(order is null)
            return false;

        var item = order.Products.FirstOrDefault(x => x.Id.Equals(itemId));

        if(item is null)
            return false;

        item.IsCancelled = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}