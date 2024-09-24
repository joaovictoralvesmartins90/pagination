using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pagination.Context;
using pagination.Models;
using pagination.Queries;

namespace pagination.Controllers;

[ApiController]
[Route("api/items")]
public class ItensController(AppDbContext appDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMatchingStuff([FromQuery] ItemQuery itemQuery)
    {
        var searchPhraseLower = itemQuery.SearchPhrase.ToLower();

        var baseQuery = appDbContext.Items.Where(i => searchPhraseLower == null
            || (i.PropertyOne.ToLower().Contains(searchPhraseLower))
            || (i.PropertyTwo.ToLower().Contains(searchPhraseLower)));

        var totalItems = await baseQuery.CountAsync();

        var items = await baseQuery
            .Skip(itemQuery.PageSize * (itemQuery.PageNumber - 1))
            .Take(itemQuery.PageSize)
            .ToListAsync();

        List<ItemDto> itemsDto = items.Select(a => new ItemDto()
        {
            PropertyOne = a.PropertyOne,
            PropertyTwo = a.PropertyTwo,
        }).ToList();

        var results = new PagedResult<ItemDto>(itemsDto, totalItems, itemQuery.PageSize, itemQuery.PageNumber);

        return Ok(results);

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