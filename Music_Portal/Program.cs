using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;
using Music_Portal;
using Microsoft.AspNetCore.Http.Features;
using MusicPortal.DAL.Context;

var builder = WebApplication.CreateBuilder(args);


// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");



// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // ������������ ������ (����-��� ���������� ������)
    options.Cookie.Name = "Session"; // ������ ������ ����� ���� �������������, ������� ����������� � �����.

});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
