using Company.Day02.BLL.Interfaces;
using Company.Day02.BLL.Repositories;
using Company.Day02.DAL.Data.Contexts;
using Company.Day02.DAL.Models;
using Company.Day02.PL.Mapping;
using Company.Day02.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Company.Day02.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            
            //builder.Services.AddScoped<IDepartmentRepository ,  DepartmentRepository>(); // Allow DI For DepartmentRepository


            //builder.Services.AddScoped<IEmployeeRepository ,  EmployeeRepository>(); // Allow DI For EmployeeRepository

            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // if you have more than one profile you should allow dependancy injection for each one 

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            //these all three function to allow dependancy injection for specific services : they are different in life time 
            //builder.Services.AddScoped(); // create object life time per request
            //builder.Services.AddTransient(); // create object life time per operation 
            //builder.Services.AddSingleton(); // create object life time per application

            builder.Services.AddScoped<IScopedService, ScopedService>(); // per request
            builder.Services.AddTransient<ITransentService , TransentService>(); // per operation
            builder.Services.AddSingleton<ISingletonService , Singletonservice>();//per application 

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>();


            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
