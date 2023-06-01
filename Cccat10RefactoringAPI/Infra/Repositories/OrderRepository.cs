using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Dapper;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        //const string sql = "INSERT INTO Order (Id, CPF, From, To) VALUES (@Id, @CPF, @From, @To)";
        //using var connection = await SQLHelper.GetConnection();
        //await connection.ExecuteAsync(sql, order);
        //return order;
        throw new NotImplementedException();
    }

    public async Task<Order?> GetOrderAsync(Guid id)
    {
        const string sql = @"SELECT * FROM ""Order""
LEFT JOIN ""OrderItem"" ON ""Order"".Id = ""OrderItem"".OrderId
LEFT JOIN ""Product"" ON ""OrderItem"".ProductId = ""Product"".Id
LEFT JOIN ""Coupon"" ON ""Order"".CouponId = ""Coupon"".Id
WHERE ""Order"".Id = @Id";

        using var connection = await SQLHelper.GetConnection();
        Order? order = null;
        var result = await connection.QueryAsync<Order, OrderItem, Product, Coupon, Order>(sql,
            (orderEntry, item, product, coupon) =>
            {
                order ??= orderEntry;
                item.Product = product;
                order.Coupon = coupon;
                order.AddItem(item);
                return order;
            },
            new { Id = id },
            splitOn: "Id");
        return order;
    }
}
