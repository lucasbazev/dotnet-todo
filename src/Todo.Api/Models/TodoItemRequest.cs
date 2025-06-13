using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Models
{
    // DTO used for creating or updating a TodoItem
    public class TodoItemRequest
    {
        [Required(ErrorMessage = "The title is required.")]
        [StringLength(250, ErrorMessage = "The title must be between 1 and 250 characters long.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }

    public class TodoItemPatchRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
