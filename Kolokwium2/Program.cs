using ArtApi.Services;
using Kolokwium2.Data;
using Kolokwium2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();

var app = builder.Build();
app.MapControllers();
app.Run();