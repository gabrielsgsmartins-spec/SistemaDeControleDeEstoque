using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;
using SistemaEstoque.Models;
using SistemaEstoque.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EstoqueContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoPadrao")
    ));

// Identity
builder.Services.AddIdentity<ApplicationUserModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<EstoqueContext>()
.AddDefaultTokenProviders();

// Configuração do Login
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
});

// Repositórios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();




app.Run();