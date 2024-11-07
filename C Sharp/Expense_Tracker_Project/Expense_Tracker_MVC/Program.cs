using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Expense_Tracker_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddTransient<ICategoryService,CategoryService>();

            builder.Services.AddTransient<IBudgetService, BudgetService>();

            builder.Services.AddTransient<IExpenseService, ExpenseService>();

            builder.Services.AddTransient<ICategorySpendService, CategorySpendService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddHttpClient<IUserService, UserService>(c =>
            c.BaseAddress = new Uri("https://localhost:7273/"));

            //1.configure the Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(options =>
                        {
                            options.LoginPath = "/Login/Login";
                            options.Cookie.Name = ".AspNetCore.Cookies";
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                            options.SlidingExpiration = true;

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
