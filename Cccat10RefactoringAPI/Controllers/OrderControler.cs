using Microsoft.AspNetCore.Mvc;

namespace Cccat10RefactoringAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderControler : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(";-)");
    }
}