using System.Linq;
using Raven.Client.Indexes;

namespace Models
{
    public class AnimalsAndVehicles : AbstractMultiMapIndexCreationTask
    {
        public class CommonMap
        {
            public int TopSpeed { get; set; }

            public string Location { get; set; }
        }

        public AnimalsAndVehicles()
        {
            AddMap<Animal>(animals => 
                from animal in animals
                select new
                            {
                                TopSpeed = animal.TopSpeed,
                                Location = animal.Habitat,
                            });

            AddMap<Vehicle>(vehicles => 
                from vehicle in vehicles
                select new
                            {
                                TopSpeed = vehicle.TopSpeed,
                                Location = vehicle.WorksOn
                            });
        }
    }
}