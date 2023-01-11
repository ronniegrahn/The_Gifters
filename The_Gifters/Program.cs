using Microsoft.EntityFrameworkCore;
using The_Gifters.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GiftersContext>(o => o.UseSqlServer(connString));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(o => o.MapControllers());

app.UseHttpsRedirection();

app.Run();
