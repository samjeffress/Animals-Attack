using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HtmlAgilityPack;
using Models;
using Raven.Client.Linq;

namespace Web.Controllers
{
    public class HomeController : RavenController
    {
        public ActionResult Index()
        {
            var images =  RavenSession.Query<Animal>()
                .Where(a => a.Status == "Living")
                .Where(a => a.ImageAddress.Length > 0)
                .Select(a => new { a.ImageAddress, a.Name })
                .ToList();

            var imageList = new List<MvcHtmlString>();
            foreach (var image in images)
            {
                var html = new HtmlDocument();
                html.LoadHtml(image.ImageAddress);
                var imgSrc = html.DocumentNode.Descendants("img")
                    .Select(e => e.GetAttributeValue("src", null))
                    .Where(s => !string.IsNullOrEmpty(s))
                    .FirstOrDefault();
                imageList.Add(new MvcHtmlString(string.Format("<img src='{0}' alt='{1}' />", imgSrc, image.Name)));
            }
            ViewBag.Images = imageList;

            return View();
        }
    }
}
