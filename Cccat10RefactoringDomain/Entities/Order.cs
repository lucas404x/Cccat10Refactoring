using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.Entities;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _items;

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Total { get; private set; }
    public double FeeTax { get; private set; }
    public CPF CPF { get; private set; }
    public Coupon? Coupon { get; private set; }

    public Order(decimal total, double feeTax, List<OrderItem> items, CPF cpf, Coupon? coupon)
    {
        Total = total;
        FeeTax = feeTax;
        Coupon = coupon;
        CPF = cpf;
        _items = items;
    }
}
