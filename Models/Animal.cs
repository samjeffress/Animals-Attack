using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models
{
    public class Animal
    {
        public string Name { get; set; }

        [Required]
        public List<string> Habitat { get; set; }

        public int TopSpeed { get; set; }

        public string Status { get; set; }

        [AllowHtml]
        public string ImageAddress { get; set; }
    }
}