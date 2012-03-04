using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models;
using Raven.Client.Linq;

namespace Web.Controllers
{
    public class AnimalsController : RavenController
    {
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Animal> animals = RavenSession.Query<Animal>()
                    .Where(a => a.Status == "Living")
                    .ToList();
                return View("Index", animals);
            }
            catch(Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }
        }

        public ActionResult Edit(string name)
        {
            var animal = RavenSession.Query<Animal>()
                .Where(a => a.Name == name)
                .FirstOrDefault();
            return View("Edit", animal);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            var name = collection["Name"];
            
            var animal = RavenSession.Load<Animal>(name);
            //if (TryUpdateModel(animal, collection))
            //    throw new ArgumentException("Bad input");
            var topSpeed = collection["TopSpeed"];
            animal.TopSpeed = Convert.ToInt32(topSpeed);
            animal.ImageAddress = collection["ImageAddress"];
            return Index();
        }
    }
}
