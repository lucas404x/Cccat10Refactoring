using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.Repositories;

public interface IProductRepository
{
    public Task<Product?> GetProductAsync(long id);
}
