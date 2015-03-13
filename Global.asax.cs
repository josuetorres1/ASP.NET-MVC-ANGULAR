using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using AngularJSProofofConcept.App_Start;
//using Microsoft.AspNet.SignalR;
using Microsoft.CSharp;
//using Microsoft.Owin;
//using Owin;

//[assembly: OwinStartup(typeof(AngularJSProofofConcept.MvcApplication))]
namespace AngularJSProofofConcept
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //SqlDependency.Start(@"Data Source=(localdb)\v11.0;Initial Catalog=e2rm_dev;Integrated Security=true");

        }

        //protected void Application_End()
        //{
        //    //Free the dependency
        //    SqlDependency.Stop(@"Data Source=(localdb)\v11.0;Initial Catalog=e2rm_dev;Integrated Security=true");
        //}

        //public void Configuration(IAppBuilder app)
        //{
        //    app.MapSignalR();
        //}
    }
}