using Cccat10RefactoringAPI.Models;
using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cccat10RefactoringAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;

    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var response = new ApiResponse<Coupon>();
        try
        {
            response.Result = await _couponRepository.GetCouponAsync(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Coupon coupon)
    {
        var response = new ApiResponse<Coupon>();
        try
        {
            response.Result = await _couponRepository.CreateCouponAsync(coupon);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
