using System.Collections.Generic;
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
            LoadData();
        }

        public DocumentStore DocumentStore { get; set; }

        private void LoadData()
        {
            using (var session = DocumentStore.OpenSession())
            {
                var animalList = new List<Animal>();
                animalList.Add(new Animal { Name = "Tiger", Habitat = "Land", Status = "Living", TopSpeed = 30 });
                animalList.Add(new Animal { Name = "Teradactal", Habitat = "Sky", Status = "Extinct", TopSpeed = 300 });
                animalList.Add(new Animal { Name = "Whale", Habitat = "Ocean", Status = "Living", TopSpeed = 10 });
                animalList.Add(new Animal { Name = "Ferret", Habitat = "Land", Status = "Living", TopSpeed = 9 });
                animalList.Add(new Animal { Name = "TRex", Habitat = "Land", Status = "Extinct", TopSpeed = 5 });
                animalList.Add(new Animal { Name = "Shark", Habitat = "Ocean", Status = "Living", TopSpeed = 25 });
                animalList.Add(new Animal { Name = "Eagle", Habitat = "Sky", Status = "Living", TopSpeed = 32 });

                foreach (var animal in animalList)
                    session.Store(animal, animal.Name);


                var vehicleList = new List<Vehicle>();
                vehicleList.Add(new Vehicle { Name = "Car", WorksOn = "Land", TopSpeed = 60, Colour = "Red" });
                vehicleList.Add(new Vehicle { Name = "Speedboat", WorksOn = "Ocean", TopSpeed = 30, Colour = "Blue" });
                vehicleList.Add(new Vehicle { Name = "Train", WorksOn = "Land", TopSpeed = 20, Colour = "Black" });
                vehicleList.Add(new Vehicle { Name = "Kayak", WorksOn = "Ocean", TopSpeed = 8, Colour = "Red" });
                vehicleList.Add(new Vehicle { Name = "Pushbike", WorksOn = "Land", TopSpeed = 12, Colour = "Pink" });

                foreach (var vehicle in vehicleList)
                    session.Store(vehicle, vehicle.Name);

                session.SaveChanges();
            }
        }
    }
}
