using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Dapper;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        const string sqlOrderInsert = @"INSERT INTO ""Order""(""Id"", ""CPF"", ""From"", ""To"",""CouponId"") VALUES (@Id, @CPF, @From, @To, @CouponId)";
        const string sqlOrderItemInsert = @"INSERT INTO ""OrderItem""(""Id"",""Quantity"",""ProductId"",""OrderId"") VALUES (@Id,@Quantity,@ProductId,@OrderId)";

        using var connection = await SQLHelper.GetConnection();
        using var dbTransaction = await connection.BeginTransactionAsync();
        try
        {
            var result = await connection.ExecuteAsync(sqlOrderInsert, new
            {
                order.Id,
                order.CPF,
                order.From,
                order.To,
                CouponId = order.Coupon?.Id,
            }, dbTransaction);
            if (result == 0)
            {
                throw new InvalidDataException("Order insert operation not performed.");
            }
            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
                result = await connection.ExecuteAsync(sqlOrderItemInsert, new
                {
                    item.Id,
                    item.Quantity,
                    ProductId = item.Product.Id,
                    item.OrderId,
                }, dbTransaction);
                if (result == 0)
                {
                    throw new InvalidDataException("Item insert operation not performed.");
                }
            }
            await dbTransaction.CommitAsync();
            return order;
        }
        catch (Exception)
        {
            dbTransaction.Rollback();
            throw;
        }
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
