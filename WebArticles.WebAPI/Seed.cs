using System;
using WebArticles.DataModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI;

namespace WebArticles.WebAPI.Infrastructure
{
    public static class Seed
    {
        public static async Task SeedUsers(ArticleDbContext context, UserManager<User> userManager)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    UserName = "incorcadit",
                    FirstName = "Alex",
                    LastName = "Mayson",
                    Email = "alex@mail.com",
                    ProfilePickLink = "https://yt3.ggpht.com/a/AATXAJx7ni8OWumF2f6gmEF9A2Uncy1DcX-fglMCdA=s900-c-k-c0xffffffff-no-rj-mo",
                    BirthDate = new DateTime(1999, 8, 11),
                    ExternalProvider = false,
                    Reviewer = new Reviewer()
                    {
                        ReviewerDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien."
                    },
                    Writer = new Writer()
                    {
                        WriterDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien."
                    }
                };
                await userManager.CreateAsync(user, "alex1234");

                var user2 = new User
                {
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "admin@mymail.com",
                    ProfilePickLink = "https://fiverr-res.cloudinary.com/images/q_auto,f_auto/gigs/125568526/original/cd9c93141521436a112722e8c5c0c7ba0d60a4a2/be-your-telegram-group-admin.jpg",
                    BirthDate = new DateTime(1985, 2, 15),
                    ExternalProvider = false,
                    Reviewer = new Reviewer()
                    {
                        ReviewerDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien."
                    },
                    Writer = new Writer()
                    {
                        WriterDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien."
                    }
                };
                await userManager.CreateAsync(user2, "admin1234");
            }
        }

        public static async Task SeedRoles(ArticleDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new Role { Name = "User" });
                await roleManager.CreateAsync(new Role { Name = "Admin" });
               
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("incorcadit"), "User");
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), "User");
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), "Admin");
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedTopics(ArticleDbContext context)
        {
            if (!context.Topics.Any())
            {
                context.Topics.Add(new Topic { TopicName = "Basketball" });
                context.Topics.Add(new Topic { TopicName = "IT" });
                context.Topics.Add(new Topic { TopicName = "Business" });
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedArticles(ArticleDbContext context)
        {
            if (!context.Articles.Any())
            {
                var user = context.Users.Include(u => u.Writer).ThenInclude(w => w.Articles).First(u => u.UserName == "incorcadit");

                user.Writer.Articles.Add(new Article
                {
                    Title = "New NBA Season is postponed for half of a year",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#nba#newseason",
                    Topic = context.Topics.First(t => t.TopicName == "Basketball"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 7, 24, 17, 45, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Shakil O'Neill will take part in 2020 all-start weekend",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#allstart#legends#shakil#nba#danks",
                    Topic = context.Topics.First(t => t.TopicName == "Basketball"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2020, 3, 17, 13, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Functional interfaces in Java",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#java#lambdas#it#programming",
                    Topic = context.Topics.First(t => t.TopicName == "IT"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 5, 17, 13, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Doublespending in 5 minutes",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#bitcoin#crypto#hacking#programming",
                    Topic = context.Topics.First(t => t.TopicName == "IT"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 9, 22, 17, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Cryptomarket in 2020",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#bitcoin#crypto#it#2020#cryptomarket",
                    Topic = context.Topics.First(t => t.TopicName == "IT"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 9, 22, 17, 28, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Scrapping with Scrapy",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#it#programming#scrapping#python",
                    Topic = context.Topics.First(t => t.TopicName == "IT"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 4, 22, 17, 22, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Luckin Coffee: Scandal-hit chain raided by regulators in China",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#business#china#coffee#foodchain",
                    Topic = context.Topics.First(t => t.TopicName == "Business"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 6, 18, 17, 22, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Stressed firms look for better ways to source products",
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#business#china#coffee#foodchain",
                    Topic = context.Topics.First(t => t.TopicName == "Business"),
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2020, 9, 28, 21, 52, 0)
                });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedComments(ArticleDbContext context)
        {
            if (!context.Comments.Any())
            {
                var article = context.Articles.Include(a => a.Comments).First();
                var reviewer = context.Users.Include(u => u.Reviewer).First(u => u.UserName == "admin").Reviewer;

                article.Comments.Add(new Comment
                    {
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                        Article = article,
                        Reviewer = reviewer,
                        PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                    });
                article.Comments.Add(new Comment
                    {
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                        Article = article,
                        Reviewer = reviewer,
                        PublishDate = new DateTime(2020, 8, 4, 18, 11, 0)
                    });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 12, 25, 11, 2, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2020, 3, 17, 17, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 10, 12, 22, 51, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 8, 22, 11, 15, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 8, 12, 19, 45, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 11, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    Article = article,
                    Reviewer = reviewer,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                
                await context.SaveChangesAsync();
            }
        }
    }
}
