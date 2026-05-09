using Microsoft.AspNetCore.Mvc;
using Backend.BusinessLogic.DTOs.Promotions;
using Backend.BusinessLogic.Services.Interfaces;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromotionController(IPromotionService promotionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var promotions = await promotionService.GetAllPromotionsAsync();
        return Ok(promotions);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var promotion = await promotionService.GetPromotionByIdAsync(id);
            return Ok(promotion);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PromotionCreateDto dto)
    {
        var created = await promotionService.AddPromotionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PromotionUpdateDto dto)
    {
        try
        {
            var updated = await promotionService.UpdatePromotionAsync(id, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await promotionService.DeletePromotionAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}