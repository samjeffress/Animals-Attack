Imports System.Web.Mvc
Imports Web.Microsoft.Web.Mvc
 
<Assembly: WebActivator.PreApplicationStartMethod(GetType(Web.App_Start.MobileViewEngines), "Start")>
Namespace Web.App_Start
    Public NotInheritable Class MobileViewEngines
        Public Shared Sub Start()
            ViewEngines.Engines.Insert(0, New MobileCapableRazorViewEngine())
            ViewEngines.Engines.Insert(0, New MobileCapableWebFormViewEngine())
        End Sub
    End Class
End Namespace
