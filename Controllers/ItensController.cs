using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pagination.Context;
using pagination.Models;

namespace pagination.Controllers;

[ApiController]
[Route("api/items")]
public class ItensController(AppDbContext appDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStuff()
    {
        var items = await appDbContext.Items.ToArrayAsync();
        return Ok(items);

    }

    [HttpPost]
    public async Task<IActionResult> PostStuff([FromBody] ItemDto itemDto)
    {
        Item item = new Item();
        item.PropertyOne = itemDto.PropertyOne;
        item.PropertyTwo = itemDto.PropertyTwo;
        await appDbContext.Items.AddAsync(item);
        await appDbContext.SaveChangesAsync();
        return Ok();

    }
    [HttpPatch]
    public async Task<IActionResult> PatchStuff()
    {
        return Ok();

    }
    [HttpDelete]
    public async Task<IActionResult> DeleteStuff()
    {
        return Ok();

    }
}