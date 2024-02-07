using Microsoft.AspNetCore.Mvc;
using VivesBlog.Models;
using VivesBlog.Services;

namespace VivesBlog.Cyb.Ui.Mvc.Controllers
{
    public class PeopleController(PeopleService peopleService) : Controller
    {

        [HttpGet("People/Index")]
        public IActionResult Index()
        {
            var people = peopleService.Find();
            return View(people);
        }

        [HttpGet("People/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("People/Create")]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            
            peopleService.Create(person);
            return RedirectToAction("Index");
        }

        [HttpGet("People/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var person = peopleService.Get(id);
            return View(person);
        }

        [HttpPost("People/Edit/{id}")]
        public IActionResult Edit(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            peopleService.Edit(person.Id, person);

            return RedirectToAction("Index");
        }

        [HttpGet("People/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var person = peopleService.GetSingle(id);
            return View(person);
        }

        [HttpPost("People/Delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            peopleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
