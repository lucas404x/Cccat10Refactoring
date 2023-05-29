using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Dapper;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<Order?> GetOrderAsync(Guid id)
    {
        //const string sql = @"SELECT * FROM Order WHERE Id = @Id LEFT JOIN OrderItem ON Order.Id = OrderItem.OrderId";

        //using var connection = await SQLHelper.GetConnection();
        //var result = await connection.QueryAsync<Order, OrderItem, Order>(sql,
        //    (orderEntry, item) =>
        //    {
        //        orderEntry.AddItem(item);
        //        return orderEntry;
        //    },
        //    new { Id = id },
        //    splitOn: "Id");
        //return result.FirstOrDefault();
         
        const string sql = @"SELECT * FROM Order WHERE Id = @Id";
        using var connection = await SQLHelper.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id });
    }
}
