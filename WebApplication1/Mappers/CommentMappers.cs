using WebApplication1.Dtos.Comment;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCreateCommentDto(this CreateCommentRequest commentModel, int stockId)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = DateTime.Now.ToUniversalTime(),
            StockId = stockId
        };
    }

    public static Comment ToUpdateCommentDto(this UpdateCommentRequestDto commentModel)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content
        };
    }
}