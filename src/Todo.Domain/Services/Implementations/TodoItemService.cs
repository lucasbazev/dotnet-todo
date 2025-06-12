using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;

namespace Todo.Domain.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _todoItemRepository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            TodoItem? item = await _todoItemRepository.GetByIdAsync(id);

            return item ?? throw new InvalidOperationException($"Todo item with id {id} not found.");
        }

        public async Task<TodoItem> CreateAsync(string title, string? description)
        {
            TodoItem item = new TodoItem(title, description);
            TodoItem savedItem = await _todoItemRepository.AddAsync(item);
            await _todoItemRepository.SaveChangesAsync();
            return savedItem;
        }

        public async Task<TodoItem> UpdateAsync(Guid id, string? title, string? description)
        {
            TodoItem item = await this.GetByIdAsync(id);
            item.Update(title, description);
            await _todoItemRepository.UpdateAsync(item);
            await _todoItemRepository.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem> ToggleIsCompleted(Guid id)
        {
            TodoItem item = await this.GetByIdAsync(id);
            item.ToggleIsCompleted();
            await _todoItemRepository.UpdateAsync(item);
            await _todoItemRepository.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TodoItem item = await this.GetByIdAsync(id);
            bool deleted = await _todoItemRepository.DeleteAsync(id);
            await _todoItemRepository.SaveChangesAsync();
            return deleted;
        }
    }
}
