namespace Todo.Domain.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItem(string title, string? description)
        {
            Id = Guid.NewGuid();
            IsCompleted = false;
            Title = title ?? throw new ArgumentException("Title cannot be empty.", nameof(title));
            Description = description;
        }

        // Entity Framework Core uses this empty constructor on db entity instantiation
        protected TodoItem() { }

        public void Update(string? title, string? description)
        {
            Title = title ?? this.Title;
            Description = description;
        }

        public void ToggleIsCompleted()
        {
            IsCompleted = !IsCompleted;
        }
    }
}
