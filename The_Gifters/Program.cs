
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using The_Gifters.Models;
using The_Gifters.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllersWithViews();

builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<ParticipatesService>();
builder.Services.AddDbContext<GiftersContext>(o => o.UseSqlServer(connString));
builder.Services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(connString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/users/login");

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Identifiering
app.UseAuthorization(); // Beh�righet
app.UseEndpoints(o => o.MapControllers());

app.Run();
