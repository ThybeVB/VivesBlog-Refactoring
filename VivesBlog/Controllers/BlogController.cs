using Microsoft.AspNetCore.Mvc;
using System;
using VivesBlog.Models;
using VivesBlog.Services;

namespace VivesBlog.Cyb.Ui.Mvc.Controllers
{
    public class BlogController(BlogService blogService) : Controller
    {
        [HttpGet("Blog/Index")]
        public IActionResult Index()
        {
            var articles = blogService.Find();
            return View(articles);
        }

        [HttpGet("Blog/Create")]
        public IActionResult Create()
        {
            var articleModel = blogService.CreateArticleModel();

            return View(articleModel);
        }

        [HttpPost("Blog/Create")]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = blogService.CreateArticleModel(article);
                return View(articleModel);
            }

            article.CreatedDate = DateTime.Now;

            blogService.Create(article);

            return RedirectToAction("Index");
        }

        [HttpGet("Blog/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var article = blogService.Get(id);
            var articleModel = blogService.CreateArticleModel(article);

            return View(articleModel);
        }

        [HttpPost("Blog/Edit/{id}")]
        public IActionResult Edit(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = blogService.CreateArticleModel(article);
                return View(articleModel);
            }

            blogService.Edit(article.Id, article);

            return RedirectToAction("Index");
        }

        [HttpGet("Blog/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var article = blogService.GetDelete(id);
            return View(article);
        }

        [HttpPost("Blog/Delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            blogService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
