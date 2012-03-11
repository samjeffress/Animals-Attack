using System.Collections.Generic;

namespace Models
{
    public class Vehicle
    {
        public string Name { get; set; }

        public List<string> WorksOn { get; set; }

        public int TopSpeed { get; set; }

        public string Colour { get; set; }
    }
}
