using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringDomain.Repositories;

public interface IOrderRepository
{
    public Task<Order?> GetOrderAsync(Guid id);
    public Task<Order> CreateOrderAsync(Order order);
}
