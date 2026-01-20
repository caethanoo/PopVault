using Microsoft.EntityFrameworkCore;
using PopVault.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Database Context
builder.Services.AddDbContext<AppDbContext>();

// 2. Services (Application Layer)
builder.Services.AddScoped<PopVault.Application.Services.ArtistService>();
builder.Services.AddScoped<PopVault.Application.Services.AlbumService>();

// 3. Controllers
builder.Services.AddControllers();

// 3. Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. CORS (Allow Frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Ensure DB Created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
