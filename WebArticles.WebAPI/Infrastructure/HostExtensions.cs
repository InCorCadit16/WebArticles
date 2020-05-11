using DataModel.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebAPI;

namespace WebArticles.WebAPI.Infrastructure
{
    public static class HostExtensions
    {
        public static async Task SeedDb(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ArticleDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                    await Seed.SeedUsers(context, userManager);
                    await Seed.SeedRoles(context, roleManager, userManager);
                    await Seed.SeedTopics(context);
                    await Seed.SeedArticles(context);
                    await Seed.SeedComments(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during database seeding");
                }
            }
        }
    }
}
