using MyMvcProject.BusinessLayer.Dependency_Resolver;
using MyMvcProject.CommonLayer;
using MyMvcProject.WebApp.Infrastructure.Ninject;
using MyMvcProject.WebApp.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyMvcProject.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));

            App.Common = new WebCommon();
        }
    }
}
