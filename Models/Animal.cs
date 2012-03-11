﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Models
{
    public class Animal
    {
        public string Name { get; set; }

        public List<string> Habitat { get; set; }

        public int TopSpeed { get; set; }

        public string Status { get; set; }

        [AllowHtml]
        public string ImageAddress { get; set; }
    }
}