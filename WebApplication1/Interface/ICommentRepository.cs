using WebApplication1.Models;

namespace WebApplication1.Interface;

public interface ICommentRepository
{ 
    Task<List<Comment>> GetAllAsync();

    Task<Comment?> GetByIdAsync(int id);

    Task<Comment> CreateCommentAsync(Comment comment);

    Task<Comment?> UpdateCommentAsync(int id,Comment comment);

    Task<Comment?> DeleteCommentAsync(int id);
}