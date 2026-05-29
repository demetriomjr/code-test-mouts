using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleOrderRepository
{
    /// <summary>
    /// Retrieves a paginated list of sale orders
    /// </summary>
    /// <param name="page">The page number to retrieve (1-based)</param>
    /// <param name="amountPerPage">The number of orders per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of pages and the list of orders</returns>
    Task<(int totalPages, IEnumerable<SaleOrder> orders)> GetOrders(int page, int amountPerPage, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a Sale Order by it's ID
    /// </summary>
    /// <param name="id">The sale order's id to find</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order</returns>
    Task<SaleOrder> GetOrderById(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new sale order in the repository
    /// </summary>
    /// <param name="order">The sale order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order</returns>
    Task<SaleOrder> CreateAsync(SaleOrder order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the last valid order's number persisted
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The last persisted order number</returns>
    Task<int> GetLastOrderNumber(CancellationToken cancellationToken = default);

    /// <summary>
    /// Set an order to cancelled
    /// </summary>
    /// <param name="orderId">Sale order's ID to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sucess status</returns>
    Task<bool> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Set an order to cancelled
    /// </summary>
    /// <param name="orderId">Sale order's ID to cancel</param>
    /// <param name="itemId">CProduct's ID to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sucess status</returns>
    Task<bool> CancelOrderItemAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken = default);
}