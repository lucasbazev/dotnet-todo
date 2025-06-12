using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data.Context;

namespace Todo.Infrastructure.Data.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id) ??
                   throw new KeyNotFoundException($"TodoItem with ID {id} not found."); ;
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            return item;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TodoItem item = await this.GetByIdAsync(id);
            _context.TodoItems.Remove(item);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
