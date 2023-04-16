using Cccat10RefactoringDomain.Entities;

namespace Cccat10RefactoringDomain.Models;

//public class Order : BaseEntity
//{
//    private readonly List<Product> _products = new();
//    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

//    public CPF CPF { get; set; }
//    public Coupon? Coupon { get; set; }

//    public Order(string cpf, Coupon? coupon = null)
//    {
//        CPF = new(cpf);
//        Coupon = coupon;
//    }

//    public void AddProduct(Product item)
//    {
//        if (_products.Any(x => x.Guid.Equals(item.Guid)))
//        {
//            throw new ArgumentException("Item already added.", nameof(item));
//        }
//        _products.Add(item);
//    }

//    public decimal GetSubtotalPrice()
//    {
//        if (Coupon?.ExpiredDate < DateTime.Now)
//        {
//            throw new InvalidOperationException($"The coupon is expired {Coupon.ExpiredDate}");
//        }
//        var itemsSubtotalSum = Products.Sum(x => x.Subtotal);
//        return Coupon?.ApplyDiscountTo(itemsSubtotalSum) ?? itemsSubtotalSum;
//    }

//    public double GetFeeTax() => Products.Sum(x => x.GetFeeTax());

//    public void MakeOrder()
//    {
//        if (!CPF.IsValid())
//        {
//            throw new InvalidOperationException("cpf is not valid");
//        }
//    }
//}