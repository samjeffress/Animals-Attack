using System.Collections.Generic;
using Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace AnimalsAttack
{
    public class Raven
    {
        public Raven()
        {
            DocumentStore = new DocumentStore { Url = "http://localhost:8080" };
            DocumentStore.Initialize();
            IndexCreation.CreateIndexes(typeof(AnimalsAndVehicles).Assembly, DocumentStore);
            RavenDataLoader.LoadData(DocumentStore);
        }

        public DocumentStore DocumentStore { get; set; }
        
        public static class RavenDataLoader
        {
            public static void LoadData(IDocumentStore documentStore)
            {
                using (var session = documentStore.OpenSession())
                {
                    var animalList = new List<Animal>();
                    animalList.Add(new Animal { Name = "Tiger", Habitat = new List<string> { "Land" }, Status = "Living", TopSpeed = 30, ImageAddress = @"<a href='http://www.flickr.com/photos/soundofdesign/4794989404/' title='Tiger by soundofdesign, on Flickr'><img src='http://farm5.staticflickr.com/4096/4794989404_4a67881c58_m.jpg' width='240' height='240' alt='Tiger'></a>" });
                    animalList.Add(new Animal { Name = "Teradactal", Habitat = new List<string> { "Sky" }, Status = "Extinct", TopSpeed = 300 });
                    animalList.Add(new Animal { Name = "Whale", Habitat = new List<string> { "Ocean" }, Status = "Living", TopSpeed = 10, ImageAddress = @"<a href='http://www.flickr.com/photos/soundofdesign/4797060593/' title='Killer Whale by soundofdesign, on Flickr'><img src='http://farm5.staticflickr.com/4120/4797060593_975a1d723d_m.jpg' width='240' height='240' alt='Killer Whale'></a>" });
                    animalList.Add(new Animal { Name = "Ferret", Habitat = new List<string> { "Land" }, Status = "Living", TopSpeed = 9 });
                    animalList.Add(new Animal { Name = "TRex", Habitat = new List<string> { "Land" }, Status = "Extinct", TopSpeed = 5 });
                    animalList.Add(new Animal { Name = "Shark", Habitat = new List<string> { "Ocean" }, Status = "Living", TopSpeed = 25, ImageAddress = @"<a href='http://www.flickr.com/photos/soundofdesign/4797688928/' title='Shark by soundofdesign, on Flickr'><img src='http://farm5.staticflickr.com/4076/4797688928_e8f370781b_m.jpg' width='240' height='240' alt='Shark'></a>'" });
                    animalList.Add(new Animal { Name = "Eagle", Habitat = new List<string> { "Sky" }, Status = "Living", TopSpeed = 32 });

                    foreach (var animal in animalList)
                        session.Store(animal, animal.Name);


                    var vehicleList = new List<Vehicle>();
                    vehicleList.Add(new Vehicle { Name = "Car", WorksOn = new List<string> { "Land" }, TopSpeed = 60, Colour = "Red" });
                    vehicleList.Add(new Vehicle { Name = "Speedboat", WorksOn = new List<string> { "Ocean" }, TopSpeed = 30, Colour = "Blue" });
                    vehicleList.Add(new Vehicle { Name = "Train", WorksOn = new List<string> { "Land" }, TopSpeed = 20, Colour = "Black" });
                    vehicleList.Add(new Vehicle { Name = "Kayak", WorksOn = new List<string> { "Ocean" }, TopSpeed = 8, Colour = "Red" });
                    vehicleList.Add(new Vehicle { Name = "Pushbike", WorksOn = new List<string> { "Land" }, TopSpeed = 12, Colour = "Pink" });

                    foreach (var vehicle in vehicleList)
                        session.Store(vehicle, vehicle.Name);

                    session.SaveChanges();
                }
            }
        }
    }
}
