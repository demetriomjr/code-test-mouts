using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleOrderRepository : ISaleOrderRepository
{
    private readonly DefaultContext _context;

    public SaleOrderRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<(int totalPages, IEnumerable<SaleOrder> orders)> GetOrders(int page, int amountPerPage, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.Skip((page - 1) * amountPerPage).Take(amountPerPage).ToListAsync(cancellationToken);
        var totalPages = await _context.SaleOrders.CountAsync(cancellationToken) / amountPerPage;
        return (totalPages, result);
    }

    public async Task<SaleOrder> GetOrderById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        return result;
    }

    public async Task<SaleOrder> CreateAsync(SaleOrder order, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<int> GetLastOrderNumber(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.OrderByDescending(x => x.OrderNumber)
                                              .FirstOrDefaultAsync(cancellationToken);

        if(result is null)
            return 0;

        return result.OrderNumber;
    }

    public async Task<bool> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(orderId), cancellationToken);

        if(order is null)
            return false;

        order.IsCancelled = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

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