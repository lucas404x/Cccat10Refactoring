using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringDomain.Usecases;

public class CreateOrder
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;

    public CreateOrder(
        IOrderRepository orderRepository, 
        IProductRepository productRepository,
        ICouponRepository couponRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _couponRepository = couponRepository;
    }

    public async Task<Order> Execute(CreateOrderDTO input)
    {
        var CPF = new CPF(input.CPF);
        if (!CPF.IsValid())
        {
            throw new ArgumentException("CPF is not valid.");
        }
        if (input.Items.Count == 0)
        {
            throw new ArgumentOutOfRangeException("At least one product must be provided.");
        }
        Coupon? coupon = null;
        if (input.CouponId != null)
        {
            coupon = await _couponRepository.GetCouponAsync((Guid)input.CouponId);
            if (coupon == null)
            {
                throw new ArgumentException("The provided coupon does not exist.");
            }
            if (coupon.IsDateExpired())
            {
                throw new ArgumentException("The provided coupon is invalid.");
            }
        }
        var items = new List<OrderItem>();
        foreach (var item in input.Items)
        {
            var product = await _productRepository.GetProductAsync(item.Id);
            if (product == null)
            {
                throw new InvalidOperationException("One of the products was not found.");
            }
            items.Add(new OrderItem
            {
                Product = product,
                Quantity = item.Quantity,
            });
        }
        var order = new Order(CPF, coupon, input.From, input.To, items);
        return await _orderRepository.CreateOrderAsync(order);
    }
}
