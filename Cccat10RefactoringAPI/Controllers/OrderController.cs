using Cccat10RefactoringAPI.Models;
using Cccat10RefactoringDomain.DTOs;
using Cccat10RefactoringDomain.Entities;
using Cccat10RefactoringDomain.Repositories;
using Cccat10RefactoringDomain.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace Cccat10RefactoringAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;

    public OrderController(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICouponRepository couponRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _couponRepository = couponRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var response = new ApiResponse<Order>();
        try
        {
            response.Result = await _orderRepository.GetOrderAsync(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpGet("SimulateFeeTax/{id}")]
    public async Task<IActionResult> SimulateFeeTaxAsync([FromRoute] Guid id)
    {
        var response = new ApiResponse<double>();
        try
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                response.ErrorMessage = "The requested Order does not exist.";
                return NotFound(response);
            }
            response.Result = order.Items.Sum(x => x.GetFeeTax());
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateOrderDTO input)
    {
        var response = new ApiResponse<Order>();
        var usecase = new CreateOrder(_orderRepository, _productRepository, _couponRepository);
        try
        {
            response.Result = await usecase.Execute(input);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}