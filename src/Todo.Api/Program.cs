using Todo.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TodoDatabase");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'TodoDatabase' is not configured.");
}

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

