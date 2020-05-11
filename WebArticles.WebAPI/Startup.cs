using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using WebAPI.Data.Repositories;
using WebArticles.WebAPI.Data.Services;
using WebArticles.WebAPI.Data.Profiles;

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
            services.AddDbContext<ArticleDbContext>();
            services.AddScoped<IRepository, DbRepository>();
            
            services.AddScoped<ArticleService>();
            services.AddScoped<TopicService>();    
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<CommentService>();

            services.AddAutoMapper( typeof(ArticleProfile),
                                    typeof(ArticlePreviewProfile),
                                    typeof(ArticleCreateProfile),
                                    typeof(UserRegisterProfile),
                                    typeof(UserProfile),
                                    typeof(CommentProfile),
                                    typeof(CommentCreateProfile));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ArticleDbContext>();

            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

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
