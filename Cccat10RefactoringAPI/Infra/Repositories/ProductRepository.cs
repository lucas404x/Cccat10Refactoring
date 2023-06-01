using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;

namespace Cccat10RefactoringAPI.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product?> GetProductAsync(long id)
    {
        throw new NotImplementedException();
    }
}
