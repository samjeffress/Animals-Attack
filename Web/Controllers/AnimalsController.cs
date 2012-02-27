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
            IEnumerable<Animal> animals = RavenSession.Query<Animal>()
                .Where(a => a.Status == "Living")
                .ToList();
            return View("Index", animals);
        }

        public ActionResult Edit(string name)
        {
            var animal = RavenSession.Query<Animal>()
                .Where(a => a.Name == name)
                .FirstOrDefault();
            return View("Edit", animal);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
            var name = collection["Name"];
            
            var animal = RavenSession.Load<Animal>(name);
            animal.TopSpeed = Convert.ToInt32(collection["TopSpeed"]);
            animal.ImageAddress = Server.HtmlEncode(collection["ImageAddress"]);
            return Index();
        }
    }
}
