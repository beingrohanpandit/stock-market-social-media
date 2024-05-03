using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Interface;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;
    
    // Constructor takes Db Context from the ApplicationDbContext.
    public StockController(ApplicationDbContext context, IStockRepository stockRepo)
    {
        this._context = context;
        this._stockRepo = stockRepo;
    }

    // Get result.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepo.GetAllAsync();
        var stockDtos = stocks.Select(s => s.ToStockDto());
        return Ok(stockDtos);
    }

    // Get result by id.
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);

        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }
    
    // Post Request.
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequest stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    // Update Request.
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestDto updateDto)
    {
        var stock = await _stockRepo.UpdateAsync(id,updateDto);

        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }
    
    // Delete Request
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepo.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}