using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cccat10RefactoringDomain.Usecases;

public class CreateOrder
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrder(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Order> Execute(CreateOrderDTO input)
    {
        if (!input.CPF.IsValid())
        {
            throw new ArgumentException("CPF is not valid.");
        }
        if (input.Coupon?.IsDateExpired() ?? true)
        {
            throw new ArgumentException("The Coupon is not valid.");
        }
        if (input.Items.Count == 0)
        {
            throw new ArgumentOutOfRangeException("At least one product must be provided.");
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
        var order = new Order(input.CPF, input.Coupon, input.From, input.To, items);
        return await _orderRepository.CreateOrderAsync(order);
    }
}
