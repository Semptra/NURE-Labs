using System;
using System.Linq;
using System.Web.Mvc;
using Lab2.Web.Database;
using Lab2.Web.Models;

namespace Lab2.Web.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly DbContext context;

        public AnimalsController(DbContext context)
        {
            this.context = context;
        }

        [Route("animals")]
        public ActionResult AnimalsList()
        {
            var model = context.Animals.Get();
            return View("AnimalsListView", model);
        }

        [Route("animals/create")]
        public ActionResult AnimalsCreate()
        {
            return View("AnimalsCreateView");
        }

        [HttpPost]
        [Route("animals/create")]
        public ActionResult AnimalsCreate(Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Id = Guid.NewGuid();
                context.Animals.Add(animal);
                return RedirectToAction("AnimalsList");
            }
            else
            {
                return View("AnimalsCreateView", animal);
            }
        }

        [Route("animals/edit")]
        public ActionResult AnimalsEdit(Guid id)
        {
            var model = context.Animals.Get().FirstOrDefault(x => x.Id == id);
            return View("AnimalsEditView", model);
        }

        [HttpPost]
        [Route("animals/edit")]
        public ActionResult AnimalsEdit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                context.Animals.Update(animal);
                return RedirectToAction("AnimalsList");
            }
            else
            {
                return View("AnimalsEditView", animal.Id);
            }
        }

        [Route("animals/delete")]
        public ActionResult AnimalsDelete(Guid id)
        {
            var animal = context.Animals.Get().FirstOrDefault(x => x.Id == id);
            context.Animals.Delete(animal);
            return RedirectToAction("AnimalsList");
        }
    }
}