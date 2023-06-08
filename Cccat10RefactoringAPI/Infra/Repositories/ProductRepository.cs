using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Dapper;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    public async Task<Product?> GetProductAsync(Guid id)
    {
        var connection = await SQLHelper.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Product>(@"SELECT * FROM ""Product"" WHERE Id = @Id", new { Id = id });
    }
}
