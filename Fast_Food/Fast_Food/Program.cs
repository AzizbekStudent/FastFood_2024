using Fast_Food.DAL.Data;
using Fast_Food.DAL.Interface;
using Fast_Food.DAL.Models;
using Fast_Food.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = builder.Configuration;
string connStr = config.GetConnectionString("FastFood_2024")
    .Replace("|DataDirectory|", builder.Environment.ContentRootPath);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FastFood_DbContext>(options =>
    options.UseSqlServer(connStr)
);


// Repository initilazation
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();


// =========================================

// app build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
