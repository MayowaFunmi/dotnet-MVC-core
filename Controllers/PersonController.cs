using Microsoft.AspNetCore.Mvc;
using MvcCoreTutorial.Models.Domain;

namespace MvcCoreTutorial.Controllers
{
    public class PersonController : Controller
    {
        private readonly DatabaseContext _ctx;

        public PersonController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.greeting = "Hello World from ViewBag";
            ViewData["greeting2"] = "This is coming from ViewData";
            TempData["greeting3"] = "And this is the TempData Data";
            return View();
        }

        // a get method
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Person.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "Person added successfully!!!";
                return RedirectToAction("AddPerson");
            }
            catch (Exception)
            {
                TempData["msg"] = "Data cannot be added";
                return View();
            }
        }

        public IActionResult DisplayPersons()
        { 
            var persons = _ctx.Person.ToList();
            return View(persons);
        }

        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Person.Update(person);
                _ctx.SaveChanges();
                return RedirectToAction("DisplayPersons");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Person cannot be updated";
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditPerson(int id)
        {
            var person = _ctx.Person.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return RedirectToAction("DisplayPersons");
            }
            return View(person);
        }


        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _ctx.Person.Find(id);
                if (person != null)
                {
                    _ctx.Person.Remove(person);
                    _ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("DisplayPersons");
        }
    }
}
