using Microsoft.OpenApi.Models;
using AutoMapper;
using BlogDAL.Repository;
using BlogDAL.Models;
using BlogBLL;
using BlogBLL.Ext;
using BlogDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace BlogApi
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                options =>
                {
                    var basePath = AppContext.BaseDirectory;

                    var xmlPath = Path.Combine(basePath, "BlogApi.xml");
                    options.IncludeXmlComments(xmlPath);
                    options.SwaggerDoc(
                        "v1", new OpenApiInfo
                        {
                            Version = "v1",
                            Title = "Shop API",
                            Description = "Пример ASP .NET Core Web API",
                            Contact = new OpenApiContact
                            {
                                Name = "Пример контакта",
                                Url = new Uri("https://example.com/contact")
                            },
                            License = new OpenApiLicense
                            {
                                Name = "Пример лицензии",
                                Url = new Uri("https://example.com/license")
                            }
                        });
                });

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var connectionSql = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(connectionSql);
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

            builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();

            app.Run();
        }
    }
}
