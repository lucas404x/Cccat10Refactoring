using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Repositories;

namespace Cccat10RefactoringDomain.Usecases;

public class Checkout
{
    private readonly IOrderRepository _orderRepository;

    public Checkout(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<CheckoutResultDTO> Execute(Guid orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order doesn't exist.");
        }
        if (order.Items.Count == 0)
        {
            throw new ArgumentOutOfRangeException("Order must have at least one item.");
        }
        if (!order.CPF.IsValid())
        {
            throw new ArgumentException("CPF is not valid.");
        }
        var output = new CheckoutResultDTO
        {
            Total = order.GetTotal(),
            FeeTax = order.Items.Sum(x => x.GetFeeTax())
        };
        if (order.Coupon != null)
        {
            if (order.Coupon.IsDateExpired())
            {
                throw new InvalidOperationException("The requested coupon is expired.");
            }
            output.Total = order.Coupon.ApplyDiscountTo(output.Total);
        }
        if (!string.IsNullOrWhiteSpace(order.From) && !string.IsNullOrWhiteSpace(order.To))
        {
            output.Total += (decimal)output.FeeTax;
        }
        return output;
    }
}
