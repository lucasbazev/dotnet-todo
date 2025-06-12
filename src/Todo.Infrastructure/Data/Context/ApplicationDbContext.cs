using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        // constructor to pass options to the DB, such as connection config, etc
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // this will map the entity 'TodoItem' to the 'TodoItems' table in the database
        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        // entity mapping configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>(item =>
            {
                // marking TodoItem.Id as the primary key
                item.HasKey(t => t.Id);

                // do not generate TodoItem.Id on add, it will be assigned automatically by the class contructor (type Guid)
                item.Property(t => t.Id)
                    .ValueGeneratedNever();

                // setting Title as required with a max length of 200 characters
                item.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
