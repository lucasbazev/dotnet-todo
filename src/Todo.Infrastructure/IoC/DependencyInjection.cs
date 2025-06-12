using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;
using Todo.Domain.Services.Implementations;
using Todo.Infrastructure.Data.Context;
using Todo.Infrastructure.Data.Repositories;

namespace Todo.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Register DbContext with the provided connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString)); // PostgreSQL

            // Register repositories
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            // Register services
            services.AddScoped<ITodoItemService, TodoItemService>();

            return services;
        }
    }
}
