using System;
using DataModel.Data.Entities;
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
            if (!context.Users.Any(u => u.UserName == "incorcadit"))
            {
                var user = new User {
                    UserName = "incorcadit",
                    FirstName = "Alex",
                    LastName = "Mayson",
                    Email = "alex@mail.com",
                    ProfilePickLink = "https://yt3.ggpht.com/a/AATXAJx7ni8OWumF2f6gmEF9A2Uncy1DcX-fglMCdA=s900-c-k-c0xffffffff-no-rj-mo",
                    BirthDate = new DateTime(1999, 8, 11)
                };
                await userManager.CreateAsync(user, "alex1234");

                var reviewer = new Reviewer() { 
                    UserId = user.Id,
                    User = user,
                    ReviewerDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                                    dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat
                                    blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada
                                    fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor
                                    id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet
                                    massa tincidunt nunc pulvinar sapien."
                };
                var writer = new Writer() {
                    UserId = user.Id,
                    User = user,
                    WriterDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                                    dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat
                                    blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada
                                    fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor
                                    id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet
                                    massa tincidunt nunc pulvinar sapien."
                };

                user.Reviewer = reviewer;
                user.Writer = writer;

                context.Add(reviewer);
                context.Add(writer);
                context.Update(user);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new User
                {
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "admin@mymail.com",
                    ProfilePickLink = "https://yt3.ggpht.com/a/AATXAJx7ni8OWumF2f6gmEF9A2Uncy1DcX-fglMCdA=s900-c-k-c0xffffffff-no-rj-mo",
                    BirthDate = new DateTime(1985, 2, 15)
                };
                await userManager.CreateAsync(user, "admin1234");

                var reviewer = new Reviewer()
                {
                    UserId = user.Id,
                    User = user,
                    ReviewerDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et" +
                                    "dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat" +
                                    "blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada" +
                                    "fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor" +
                                    "id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet" +
                                    "massa tincidunt nunc pulvinar sapien."
                };
                var writer = new Writer()
                {
                    UserId = user.Id,
                    User = user,
                    WriterDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et" +
                                    "dolore magna aliqua.Sit amet consectetur adipiscing elit ut.Ut enim blandit volutpat maecenas volutpat" +
                                    "blandit aliquam etiam.Euismod in pellentesque massa placerat duis ultricies lacus sed.Et malesuada" +
                                    "fames ac turpis egestas maecenas pharetra.Vehicula ipsum a arcu cursus.Leo a diam sollicitudin tempor" +
                                    "id eu nisl.Nibh praesent tristique magna sit amet purus gravida.Amet mauris commodo quis imperdiet" +
                                    "massa tincidunt nunc pulvinar sapien."
                };

                user.Reviewer = reviewer;
                user.Writer = writer;

                context.Add(reviewer);
                context.Add(writer);
                context.Update(user);
                context.SaveChanges();
            }
        }

        public static async Task SeedRoles(ArticleDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new Role { Name = "User" });
                await roleManager.CreateAsync(new Role { Name = "Admin" });
                await userManager.AddToRoleAsync(context.Users.First(u => u.UserName == "incorcadit"), "User");
                await userManager.AddToRoleAsync(context.Users.First(u => u.UserName == "admin"), "User");
                await userManager.AddToRoleAsync(context.Users.First(u => u.UserName == "admin"), "Admin");
            }
        }

        public static async Task SeedTopics(ArticleDbContext context)
        {
            if (!context.Topics.Any())
            {
                var basketballTopic = new Topic { TopicName = "Basketball" };
                var itTopic = new Topic { TopicName = "IT" };
                var businessTopic = new Topic { TopicName = "Business" };
                await context.Topics.AddAsync(basketballTopic);
                await context.Topics.AddAsync(itTopic);
                await context.Topics.AddAsync(businessTopic);

                var user = context.Users
                            .Include(u => u.Writer)
                            .Include(u => u.Reviewer)
                            .First(u => u.UserName == "incorcadit");
      
                var writerTopic1 = new WriterTopic
                {
                    Topic = basketballTopic,
                    TopicId = basketballTopic.Id,
                    Writer = user.Writer,
                    WriterId = user.Writer.Id
                };

                var writerTopic2 = new WriterTopic
                {
                    Topic = itTopic,
                    TopicId = itTopic.Id,
                    Writer = user.Writer,
                    WriterId = user.Writer.Id
                };

                var reviewerTopic = new ReviewerTopic
                {
                    Topic = businessTopic,
                    TopicId = businessTopic.Id,
                    Reviewer = user.Reviewer,
                    ReviewerId = user.Reviewer.Id
                };

                await context.AddAsync(writerTopic1);
                await context.AddAsync(writerTopic2);
                await context.AddAsync(reviewerTopic);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedArticles(ArticleDbContext context)
        {
            if (!context.Articles.Any())
            {
                var user = context.Users.Include(u => u.Writer).First(u => u.UserName == "incorcadit");
                var basketballTopic = context.Topics.First(t => t.TopicName == "Basketball");
                var itTopic = context.Topics.First(t => t.TopicName == "IT");
                var businessTopic = context.Topics.First(t => t.TopicName == "Business");

                user.Writer.Articles.Add(new Article
                {
                    Title = "New NBA Season is postponed for half of a year",
                    Rating = 7,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#nba#newseason",
                    TopicId = basketballTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 7, 24, 17, 45, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Shakil O'Neill will take part in 2020 all-start weekend",
                    Rating = 0,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#allstart#legends#shakil#nba#danks",
                    TopicId = basketballTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2020, 3, 17, 13, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Functional interfaces in Java",
                    Rating = -3,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#java#lambdas#it#programming",
                    TopicId = itTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 5, 17, 13, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Doublespending in 5 minutes",
                    Rating = 22,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#bitcoin#crypto#hacking#programming",
                    TopicId = itTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 9, 22, 17, 21, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Cryptomarket in 2020",
                    Rating = 19,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#bitcoin#crypto#it#2020#cryptomarket",
                    TopicId = itTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 9, 22, 17, 28, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Scrapping with Scrapy",
                    Rating = 53,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#it#programming#scrapping#python",
                    TopicId = itTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 4, 22, 17, 22, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Luckin Coffee: Scandal-hit chain raided by regulators in China",
                    Rating = 13,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#business#china#coffee#foodchain",
                    TopicId = businessTopic.Id,
                    WriterId = user.Writer.Id,
                    PublishDate = new System.DateTime(2019, 6, 18, 17, 22, 0)
                });
                user.Writer.Articles.Add(new Article
                {
                    Title = "Stressed firms look for better ways to source products",
                    Rating = -12,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                   "et dolore magna aliqua. Enim nulla aliquet porttitor lacus luctus accumsan tortor. Risus at ultrices mi " +
                                   "tempus imperdiet. Eget gravida cum sociis natoque penatibus et magnis dis parturient. Metus dictum at " +
                                   "tempor commodo ullamcorper a.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Tempus quam " +
                                    "pellentesque nec nam aliquam sem et. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar " +
                                    "pellentesque. Tellus pellentesque eu tincidunt tortor aliquam nulla facilisi. Quis lectus nulla at " +
                                    "volutpat diam. Vestibulum mattis ullamcorper velit sed ullamcorper. Nibh sit amet commodo nulla facilisi " +
                                    "nullam vehicula. Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Purus viverra " +
                                    "accumsan in nisl. Lorem sed risus ultricies tristique nulla.",
                    Tags = "#business#china#coffee#foodchain",
                    TopicId = businessTopic.Id,
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
                var article = context.Articles.First();
                var user = context.Users.Include(u => u.Reviewer).First(u => u.UserName == "admin");

                article.Comments.Add(new Comment
                    {
                        Rating = 7,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                        ArticleId = article.Id,
                        ReviewerId = user.Id,
                        PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                    });
                article.Comments.Add(new Comment
                    {
                        Rating = 2,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                        ArticleId = article.Id,
                        ReviewerId = user.Id,
                        PublishDate = new DateTime(2020, 8, 4, 18, 11, 0)
                    });
                article.Comments.Add(new Comment
                {
                    Rating = 0,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 12, 25, 11, 2, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = 22,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2020, 3, 17, 17, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = -1,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 10, 12, 22, 51, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = 4,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 8, 22, 11, 15, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = 0,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 8, 12, 19, 45, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = -11,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 11, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = 8,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = 11,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                article.Comments.Add(new Comment
                {
                    Rating = -5,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                    "dolore magna aliqua. Sit amet consectetur adipiscing elit ut. Ut enim blandit volutpat maecenas volutpat " +
                                    "blandit aliquam etiam. Euismod in pellentesque massa placerat duis ultricies lacus sed. Et malesuada " +
                                    "fames ac turpis egestas maecenas pharetra. Vehicula ipsum a arcu cursus. Leo a diam sollicitudin tempor " +
                                    "id eu nisl. Nibh praesent tristique magna sit amet purus gravida. Amet mauris commodo quis imperdiet " +
                                    "massa tincidunt nunc pulvinar sapien. Pellentesque id nibh tortor id aliquet lectus proin nibh. Aliquet " +
                                    "sagittis id consectetur purus ut faucibus pulvinar elementum. Urna neque viverra justo nec ultrices dui. " +
                                    "Placerat in egestas erat imperdiet sed. Vitae suscipit tellus mauris a diam .Sagittis vitae et leo duis " +
                                    "ut diam quam. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et.",
                    ArticleId = article.Id,
                    ReviewerId = user.Id,
                    PublishDate = new DateTime(2019, 8, 12, 19, 5, 0)
                });
                
                await context.SaveChangesAsync();
            }
        }
    }
}
