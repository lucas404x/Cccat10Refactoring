using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.Entities;

public class Order : BaseEntity
{
    private readonly List<Product> _products;

    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    public decimal Total { get; private set; }
    public double FeeTax { get; private set; }
    public CPF CPF { get; private set; }
    public Coupon? Coupon { get; private set; }

    public Order(decimal total, double feeTax, List<Product> products, CPF cpf, Coupon? coupon)
    {
        Total = total;
        FeeTax = feeTax;
        Coupon = coupon;
        CPF = cpf;
        _products = products;
    }
}
