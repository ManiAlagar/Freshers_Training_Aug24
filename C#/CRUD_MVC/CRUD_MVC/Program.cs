using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//                                  default authentication scheme for the app.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/User/Login";//if any unauthenticated user tries to access secured URLs of the App then he will be automatically redirected this login page

});
builder.Services.AddMvc();


//This means when the login is successful then a cookie is created for the authenticated user. This cookie will be named as .ASPNetCore.Cookies.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();//files that don't change when your application is running. 
//to serve the static files like js, CSS, images, etc.

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy=Microsoft.AspNetCore.Http.SameSiteMode.Lax
};
app.UseCookiePolicy(cookiePolicyOptions);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"
    );

app.Run();

