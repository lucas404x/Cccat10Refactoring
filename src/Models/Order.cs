namespace Cccat10RefactoringCode.Models;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public CPF CPF { get; set; }
    public Coupon? Coupon { get; set; }

    public Order(string cpf, Coupon? coupon = null)
    {
        CPF = new(cpf);
        Coupon = coupon;
    }

    public void AddOrderItem(OrderItem item)
    {
        if (_items.Any(x => x.Guid.Equals(item.Guid)))
        {
            throw new ArgumentException("Item already added.", "item");
        }
        _items.Add(item);
    }

    public decimal GetSubtotalPrice()
    {
        if (Coupon?.ExpiredDate < DateTime.Now)
        {
            throw new InvalidOperationException($"The coupon is expired {Coupon.ExpiredDate}");
        }
        var itemsSubtotalSum = Items.Sum(x => x.Subtotal);
        return Coupon?.ApplyDiscountTo(itemsSubtotalSum) ?? itemsSubtotalSum;
    }

    public double GetFeeTax() => Items.Sum(x => x.GetFeeTax());

    public void MakeOrder()
    {
        if (!CPF.IsValid())
        {
            throw new InvalidOperationException("cpf is not valid");
        }
    }
}