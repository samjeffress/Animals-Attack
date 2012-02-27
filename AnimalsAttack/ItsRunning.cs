using System.Linq;
using Models;
using Raven.Client.Linq;

namespace AnimalsAttack
{
    public class ItsRunning
    {
        public static void Main(string[] args)
        {
            var raven = new Raven();

            using (var session = raven.DocumentStore.OpenSession())
            {
                var objects = session.Query<AnimalsAndVehicles.CommonMap, AnimalsAndVehicles>()
                    .Where(a => a.Location == "Land")
                    .As<dynamic>()
                    .ToList();
            }
        }
    }
}
