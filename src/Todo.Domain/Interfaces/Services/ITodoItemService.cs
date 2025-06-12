using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<TodoItem> CreateAsync(string title, string? description);
        Task<TodoItem> UpdateAsync(Guid id, string? title, string? description);
        Task<TodoItem> ToggleIsCompleted(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
