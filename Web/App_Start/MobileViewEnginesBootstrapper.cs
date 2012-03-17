using System.Web.Mvc;
using Microsoft.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(Web.App_Start.MobileViewEngines), "Start")]
namespace Web.App_Start {
    public static class MobileViewEngines{
        public static void Start() 
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
            //ViewEngines.Engines.Insert(0, new MobileCapableWebFormViewEngine());
        }
    }
}