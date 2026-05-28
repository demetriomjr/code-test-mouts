using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

public class SaleOrderRepository : ISaleOrderRepository
{
    private readonly DefaultContext _context;

    public SaleOrderRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleOrder>> GetOrders(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.ToListAsync(cancellationToken);
        return result;
    }

    public async Task<SaleOrder> GetOrderById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        return result;
    }

    public async Task<SaleOrder> CreateAsync(SaleOrder order, CancellationToken cancellationToken = default)
    {
        var result = await _context.SaleOrders.AddASync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<int> GetLastOrderNumber(SCancellationToken cancellationToken = default)
    {
        var result = await _context.SaleORders.OrderByDescending(x => x.OrderNumber)
                                              .FirstOrDefaultAsync(cancellationToken);
        return result.OrderNumber ?? 0;
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

        var item = await order.Products.FirstOrDefaultAsync(x => x.Id.Equals(itemId), cancellationToken);

        if(item is null)
            return false;

        item.IsCancelled = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}