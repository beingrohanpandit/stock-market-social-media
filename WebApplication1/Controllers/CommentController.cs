using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Comment;
using WebApplication1.Interface;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/Comments")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;

    public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
    {
        this._commentRepo = commentRepo;
        this._stockRepo = stockRepo;
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

    [HttpPost("{stockId}")]
    public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentRequest commentDto)
    {
        if (!await _stockRepo.StockExistAsync(stockId))
        {
            return BadRequest("Stock does not exist");
        }
        var commentModel = commentDto.ToCreateCommentDto(stockId);
        await _commentRepo.CreateCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody]  UpdateCommentRequestDto commentDto)
    {
        var comment = await _commentRepo.UpdateCommentAsync(id,commentDto.ToUpdateCommentDto());
        
        if (comment == null)
        {
            return NotFound("Doesn't exist");
        }

        return Ok(comment);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var comment = await _commentRepo.DeleteCommentAsync(id);
        if (comment == null)
        {
            return NotFound("Doesn't exist");
        }
        return NoContent();
    }
    
}