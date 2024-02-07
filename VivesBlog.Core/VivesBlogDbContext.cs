using Microsoft.EntityFrameworkCore;
using VivesBlog.Models;

namespace VivesBlog.Core
{
    public class VivesBlogDbContext : DbContext
    {

        public VivesBlogDbContext(DbContextOptions<VivesBlogDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Person> People { get; set; }

        public void Seed()
        {
            {
                var bavoAuthor = new Person { Id = 1, FirstName = "Bavo", Name = "Ketels" };
                var johnAuthor = new Person { Id = 2, FirstName = "John", Name = "Doe" };

                People.Add(bavoAuthor);
                People.Add(johnAuthor);

                var firstArticle = new Article
                {
                    Id = 1,
                    Title = "First article title",
                    Description = "Short description of first article",
                    Content = "The first article",
                    AuthorId = bavoAuthor.Id,
                    Author = bavoAuthor,
                    CreatedDate = DateTime.Now
                };

                var secondArticle = new Article
                {
                    Id = 2,
                    Title = "Second article title",
                    Description = "Short description of second article",
                    Content = "The second article",
                    AuthorId = johnAuthor.Id,
                    Author = johnAuthor,
                    CreatedDate = DateTime.Now.AddHours(-4)
                };

                Articles.Add(firstArticle);
                Articles.Add(secondArticle);

                SaveChanges();
            }
        }
    }
}
