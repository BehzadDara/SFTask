using Microsoft.EntityFrameworkCore;
using SnappFoodTask.Domain;
using SnappFoodTask.Domain.IRepositories;
using SnappFoodTask.Infrastructure;
using SnappFoodTask.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SnappFoodDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SnappFoodConnection")));
builder.Services.AddSingleton<DbContext>(provider => provider.GetService<SnappFoodDBContext>()!);

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowall", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors("allowall");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
