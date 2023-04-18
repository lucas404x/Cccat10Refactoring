using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Repositories;

namespace Cccat10RefactoringDomain.Usecases;

public class Checkout
{
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;

    public Checkout(
        IProductRepository productRepository,
        ICouponRepository couponRepository)
    {
        _productRepository = productRepository;
        _couponRepository = couponRepository;
    }

    public async Task<CheckoutResultDTO> Execute(CheckoutDTO input)
    {
        if (!input.CPF.IsValid())
        {
            throw new ArgumentException("CPF is not valid.", nameof(input.CPF));
        }
        var output = new CheckoutResultDTO();
        var processedProducts = new List<Guid>();
        foreach (var productCheckout in input.Products)
        {
            if (processedProducts.Contains(productCheckout.Id))
            {
                throw new InvalidOperationException("Detected duplicated products.");
            }
            var product = await _productRepository.GetProductAsync(productCheckout.Id);
            output.Total += product.Price * productCheckout.Quantity;
            output.FeeTax += product.GetFeeTax() * productCheckout.Quantity;
            processedProducts.Add(productCheckout.Id);
        }
        if (input.CouponId != null)
        {
            var coupon = await _couponRepository.GetCouponAsync((Guid)input.CouponId);
            if (coupon.IsDateExpired())
            {
                throw new InvalidOperationException("The requested coupon is expired.");
            }
            output.Total = coupon.ApplyDiscountTo(output.Total);
        }
        if (!string.IsNullOrWhiteSpace(input.From) && !string.IsNullOrWhiteSpace(input.To))
        {
            output.Total += (decimal)output.FeeTax;
        }
        return output;
    }
}
