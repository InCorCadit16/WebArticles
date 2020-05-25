using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebArticles.DataModel.Entities;
using WebArticles.WebAPI.Infrastructure;
using WebArticles.WebAPI.Data.Services;
using WebArticles.WebAPI.Data.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebArticles.WebAPI.Infrastructure.Middlewares;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebAPI.Data.Repositories.Interfaces;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ArticleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ArticlesDatabase")), ServiceLifetime.Transient);
            
            services.AddScoped<ArticleRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<TopicRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<IRepository<Writer>, EntityRepository<Writer>>();
            services.AddScoped<IRepository<Reviewer>, EntityRepository<Reviewer>>();
            
            services.AddScoped<ArticleService>();
            services.AddScoped<TopicService>();    
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<CommentService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.AllowedUserNameCharacters += " ";
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddEntityFrameworkStores<ArticleDbContext>();

            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions, Configuration);
            services.AddAuthorization();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
