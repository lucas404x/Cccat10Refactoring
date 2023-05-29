using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Dapper;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class CouponRepository : ICouponRepository
{
    public async Task<Coupon> CreateCouponAsync(Coupon coupon)
    {
        const string sql = "INSERT INTO Coupon (Id, Description, PercentDiscount, ExpiredDate) VALUES (@Id, @Description, @PercentDiscount, @ExpiredDate)";
        using var connection = await SQLHelper.GetConnection();
        await connection.ExecuteAsync(sql, coupon);
        return coupon;
    }

    public async Task<Coupon?> GetCouponAsync(Guid id)
    {
        using var connection = await SQLHelper.GetConnection();
        return await connection.QueryFirstAsync<Coupon>("SELECT * FROM Coupon WHERE Id = @Id", new { Id = id });
    }
}
