using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Helpers;
using WebApplication1.Interface;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public class StockRepository: IStockRepository
{
    private readonly ApplicationDbContext _context;
    
    public StockRepository(ApplicationDbContext context)
    {
        this._context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync(QueryObj query)
    {
        var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;

    }

    public async Task<Stock?> UpdateAsync(int id, UpdateRequestDto updateDto)
    {
        var stock =  await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }

        stock.Symbol = updateDto.Symbol;
        stock.Industry = updateDto.Industry;
        stock.Purchase = updateDto.Purchase;
        stock.LastDiv = updateDto.LastDiv;
        stock.MarketCap = updateDto.MarketCap;
        stock.CompanyName = updateDto.CompanyName;

        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> StockExistAsync(int id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }
}