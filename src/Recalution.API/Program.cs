using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recalution.Infrastructure.Data;
using Recalution.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register DbContext here
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services
    .AddIdentity<AppUser, IdentityRole>((options) =>
    {
        // Disable all password requirements
        options.Password.RequiredLength = 1;
        options.Password.RequireDigit = false; 
        options.Password.RequireUppercase = false; 
        options.Password.RequireNonAlphanumeric = false; 
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();