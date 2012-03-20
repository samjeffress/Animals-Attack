using System;
using System.Collections.Generic;
using System.Configuration;
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

        public ActionResult Create()
        {
            StatusOptionsViewBag();
            LocationOptionsViewBag(null);
            return View("Create");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            var animal = new Animal();
            //if (TryUpdateModel(animal, collection))
            //    throw new ArgumentException("Bad input");
            animal.Name = collection["Name"];
            var topSpeed = collection["TopSpeed"];
            animal.TopSpeed = Convert.ToInt32(topSpeed);
            animal.Status = collection["Status"];
            //var s = collection["Habitat"];
            //animal.Habitat = s.ToList();
            //animal.Habitat = collection["Habitat"];
            animal.ImageAddress = collection["ImageAddress"];
            RavenSession.Store(animal, animal.Name);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name)
        {
            var animal = RavenSession.Query<Animal>()
                .Where(a => a.Name == name)
                .FirstOrDefault();
            StatusOptionsViewBag();
            LocationOptionsViewBag(animal.Habitat);
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
            animal.Status = collection["Status"];
            var habitat = collection["Habitat"];
            animal.Habitat = ParseFromCsv(habitat);
            return RedirectToAction("Index");
        }

        private List<string> ParseFromCsv(string habitat)
        {
            if (habitat == null)
                return new List<string>();
            var collection = habitat.Split(',');
            return new List<string>(collection);
        }

        private void StatusOptionsViewBag()
        {
            var statusOptions = new List<SelectListItem>
                                    {
                                        new SelectListItem {Text = "Living", Value = "Living"},
                                        new SelectListItem {Text = "Extinct", Value = "Extinct"}
                                    };
            ViewBag.StatusOptions = statusOptions;
        }

        private void LocationOptionsViewBag(List<string> selectedItems)
        {
            var locations = new List<string> 
            { 
                "Ocean",
                "Land", 
            };

            var locationList = selectedItems == null ? new MultiSelectList(locations) : new MultiSelectList(locations, selectedItems);
            ViewBag.Locations = locationList;
        }
    }

    public class LocationCheckBox
    {
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
