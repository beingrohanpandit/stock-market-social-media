using WebApplication1.Dtos.Stock;
using WebApplication1.Models;

namespace WebApplication1.Interface;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();

    Task<Stock?> GetByIdAsync(int id);

    Task<Stock> CreateAsync(Stock stockModel);

    Task<Stock?> UpdateAsync(int id, UpdateRequestDto stockModel);

    Task<Stock?> DeleteAsync(int id);
}