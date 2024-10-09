using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Exchange.WebServices.Data;
using MVCwithWebApi.Web.Services;
using MVCwithWebApi.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient<IStudentService, StudentService>(c =>
c.BaseAddress = new Uri("https://localhost:7092/"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie(options =>
{
    options.LoginPath = "/api/login";
    //options.LogoutPath = "/auth/logout";
});


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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Login}/{id?}");


app.Run();
