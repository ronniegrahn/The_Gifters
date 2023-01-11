using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using The_Gifters.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<UsersService>();

// Registrera Identitys DB-kontext
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(connString));

// Konfigurera Identity att använda dess inbyggda klasser för att representera använare och roller mot vår DB-kontext
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

// Konfigurera var icke-autenticerade användare ska skickas
builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/users/login");

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Identifiering
app.UseAuthorization(); // Behörighet
app.UseEndpoints(o => o.MapControllers());

app.Run();
