using WebApplication1.Dtos.Stock;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        // It is only return the that you want to return
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequest createStockModel)
    {
        // It is only return the that you want to return
        return new Stock
        {
            Symbol = createStockModel.Symbol,
            CompanyName = createStockModel.CompanyName,
            Purchase = createStockModel.Purchase,
            LastDiv = createStockModel.LastDiv,
            Industry = createStockModel.Industry,
            MarketCap = createStockModel.MarketCap
        };
    }
}