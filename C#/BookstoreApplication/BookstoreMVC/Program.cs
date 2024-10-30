using BookstoreMVC.Services;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
//builder.Services.AddHttpClient<IBookService, BookService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
//builder.Services.AddHttpClient<IUserService, UserService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
//builder.Services.AddHttpClient<ICartService, CartService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
//builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
//builder.Services.AddHttpClient<IConfigService, ConfigService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
//builder.Services.AddHttpClient<IRoleService, RoleService>(c =>
//c.BaseAddress = new Uri("http://localhost:5287/"));
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IConfigService, ConfigService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IStatusService, StatusService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie(options =>
{
    options.LoginPath = "/login";

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
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();

app.UseCors(
       options => options.AllowAnyMethod()
   );
app.UseMvc();