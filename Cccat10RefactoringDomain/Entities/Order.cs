using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;
using System.Collections.ObjectModel;

namespace Cccat10RefactoringDomain.Entities;

public class Order : BaseEntity
{
    public CPF CPF { get; set; }
    public Coupon? Coupon { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }

    private readonly List<OrderItem> _items;
    public ReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(CPF cpf, Coupon? coupon, string? from, string? to, List<OrderItem> items)
    {
        Coupon = coupon;
        CPF = cpf;
        From = from;
        To = to;
        _items = items;
    }

    public void AddItem(OrderItem item)
    {
        if (_items.Any(x => x.Id == item.Id))
        {
            throw new ArgumentException("The item was already added.");
        }
        if (item.Quantity == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        _items.Add(item);
    }

    public decimal GetTotal() => _items.Sum(x => x.Product.Price * x.Quantity);

    public string GetCode()
    {
        string id = Id.ToString().Replace("-", "").ToUpper();
        return $"{DateTime.Now.Year}{id}";
    }
}
