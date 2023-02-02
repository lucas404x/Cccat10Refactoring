namespace Cccat10RefactoringCode.Models;

public class Order
{
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public CPF CPF { get; init; }
    public Coupon? Coupon { get; init; }

    public Order(string cpf, Coupon? coupon = null)
    {
        CPF = new(cpf);
        if (!CPF.IsValid) 
        {
            throw new ArgumentException($"The CPF {cpf} is not valid.");
        }

        Coupon = coupon;
    }

    public void AddProduct(Product product) 
    {
        _products.Add(product);
    }

    public decimal CalculatePrice()
    {
        var productsPriceSum = Products.Sum(x => x.Subtotal);
        return Coupon?.ApplyDiscountTo(productsPriceSum) ?? productsPriceSum;
    }
}