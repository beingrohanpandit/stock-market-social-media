using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.Comment;

public class CreateCommentRequest
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be 5 Character")]
    [MaxLength(280, ErrorMessage = "Title cannot be over 280 Character")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Content must be 5 Character")]
    [MaxLength(280, ErrorMessage = "Content cannot be over 280 character")]
    public string Content { get; set; } = string.Empty;
    
}