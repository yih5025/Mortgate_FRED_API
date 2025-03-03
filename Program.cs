using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MortgageWebProject.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC 및 HttpClient, 그리고 MortgageRateService DI 등록
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IMortgageRateService, MortgageRateService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// 기본 라우팅
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
