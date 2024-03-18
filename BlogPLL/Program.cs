using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BlogDAL;
using BlogDAL.Models;
using BlogBLL;
using BlogBLL.Ext;
using BlogDAL.Repository;
using NLog.Extensions.Logging;

namespace BlogPLL
{
    public abstract class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddNLog("NLog.config");

            //БД
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            builder.Services
                .AddDbContext<AppDbContext>(options => 
                {
                    options.UseSqlite(connection, b => b.MigrationsAssembly("BlogDAL"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                })                
                .AddUnitOfWork()
                .AddCustomRepository<Post, PostRepository>()
                .AddCustomRepository<Tag, TagRepository>()
                .AddCustomRepository<Remark, RemarkRepository>()
                .AddCustomRepository<PostTag, PostTagRepository>()
                .AddIdentity<User, AppRole>(opts =>
                   {
                       opts.Password.RequiredLength = 5;
                       opts.Password.RequireNonAlphanumeric = false;
                       opts.Password.RequireLowercase = false;
                       opts.Password.RequireUppercase = false;
                       opts.Password.RequireDigit = false;
                   })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();   

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("Error");
                
            }
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            const string cachePeriod = "0";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
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