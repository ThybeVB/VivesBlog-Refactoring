using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;
using VivesBlog.Models;

namespace VivesBlog.Services
{
    public class BlogService(VivesBlogDbContext dbContext)
    {
        public IList<Article> Find()
        {
            var articles = dbContext.Articles
                .Include(a => a.Author)
                .ToList();

            return articles;
        }

        public Article? Get(int id)
        {
            return dbContext.Articles.SingleOrDefault(p => p.Id == id);
        }

        public Article? Create(Article article)
        {
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();

            return article;
        }

        public Article? Edit(int id, Article article)
        {
            var dbArticle = Get(id);

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;
            dbArticle.AuthorId = article.AuthorId;

            dbContext.SaveChanges();

            return article;
        }

        public Article GetDelete(int id)
        {
            var article = dbContext.Articles
                .Include(a => a.Author)
                .Single(p => p.Id == id);

            return article;
        }

        public void Delete(int id)
        {
            var dbArticle = Get(id);

            dbContext.Articles.Remove(dbArticle);
            dbContext.SaveChanges();
        }

        public ArticleModel CreateArticleModel(Article article = null)
        {
            article ??= new Article();

            var authors = dbContext.People
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.Name)
                .ToList();

            var articleModel = new ArticleModel
            {
                Article = article,
                Authors = authors
            };

            return articleModel;
        }

    }
}
