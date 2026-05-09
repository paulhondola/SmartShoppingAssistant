using Logic.DTOs.Promotions;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/promotion")]
[ApiController]
public class PromotionController(IPromotionService promotionService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PromotionGetDto>>> GetAll()
    {
        var promotions = await promotionService.GetAllAsync();
        return Ok(promotions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PromotionGetDto>> GetById(int id)
    {
        try
        {
            var promotion = await promotionService.GetByIdAsync(id);
            return Ok(promotion);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PromotionGetDto>> Create([FromBody] PromotionCreateDto dto)
    {
        var promotion = await promotionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = promotion.Id }, promotion);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PromotionGetDto>> Update(
        int id,
        [FromBody] PromotionUpdateDto dto
    )
    {
        try
        {
            var promotion = await promotionService.UpdateAsync(id, dto);
            return Ok(promotion);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await promotionService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
