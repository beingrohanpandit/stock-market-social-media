using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    // Constructor takes Db Context from the ApplicationDbContext.
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get result.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _context.Stocks.ToListAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stockDto);
    }

    // Get result by id.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);

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
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    // Update Request.
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestDto updateDto)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stock == null)
        {
            return NotFound();
        }

        stock.Symbol = updateDto.Symbol;
        stock.Industry = updateDto.Industry;
        stock.Purchase = updateDto.Purchase;
        stock.LastDiv = updateDto.LastDiv;
        stock.MarketCap = updateDto.MarketCap;
        stock.CompanyName = updateDto.CompanyName;

        await _context.SaveChangesAsync();
        return Ok(stock.ToStockDto());
    }
    
    // Delete Request
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }

        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}