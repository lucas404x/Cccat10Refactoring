using Cccat10RefactoringAPI.Models;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cccat10RefactoringAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderControler : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderControler(IOrderRepository orderRepository) 
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(";-)");
    }
}