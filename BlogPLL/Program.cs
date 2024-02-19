using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BlogDAL.Data;
using BlogDAL.Data.Repository;
using BlogDAL.Models.Users;
using BlogPLL;
using BlogBLL.Ext;


namespace BlogPLL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //БД
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection))
                .AddUnitOfWork()
                .AddCustomRepository<Message, MessageRepository>()
                .AddCustomRepository<Friend, FriendsRepository>()
                .AddIdentity<User, IdentityRole>(opts =>
                   {
                       opts.Password.RequiredLength = 5;
                       opts.Password.RequireNonAlphanumeric = false;
                       opts.Password.RequireLowercase = false;
                       opts.Password.RequireUppercase = false;
                       opts.Password.RequireDigit = false;
                   })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            // Add services to the container.
            //builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            const string cachePeriod = "0";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");;
                }
            });
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