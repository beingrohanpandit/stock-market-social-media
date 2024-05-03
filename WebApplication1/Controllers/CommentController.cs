using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers;

[Route("api/Comments")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICommentRepository _commentRepo;

    public CommentController(ApplicationDbContext context, ICommentRepository commentRepo)
    {
        this._context = context;
        this._commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comment = await _commentRepo.GetAllAsync();
        var commentModel = comment.Select(c => c.ToCommentDto());
        return Ok(commentModel);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepo.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }
    
}