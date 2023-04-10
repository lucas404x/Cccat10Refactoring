namespace Cccat10RefactoringCode.Models;

public class Order
{
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public CPF CPF { get; set; }
    public Coupon? Coupon { get; set; }

    public Order(string cpf, Coupon? coupon = null)
    {
        CPF = new(cpf);
        Coupon = coupon;
    }

    public void AddProduct(Product product)
    {
        if (_products.Any(x => x.Equals(product)))
        {
            throw new ArgumentException("Item already added.", "product");
        }
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        if (Coupon?.ExpiredDate < DateTime.Now)
        {
            throw new InvalidOperationException($"The coupon is expired {Coupon.ExpiredDate}");
        }

        var productsPriceSum = Products.Sum(x => x.Subtotal);
        return Coupon?.ApplyDiscountTo(productsPriceSum) ?? productsPriceSum;
    }

    public decimal GetFeeTax() 
    {
        return 0;
    }

    public void MakeOrder()
    {
        if (!CPF.IsValid())
        {
            throw new ArgumentException("value not valid", "cpf");
        }
    }
}