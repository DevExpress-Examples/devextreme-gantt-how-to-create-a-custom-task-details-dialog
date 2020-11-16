using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace DevExtremeMvcApp1 {

    public class MvcApplication : HttpApplication {
       
            protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            DevExtreme.AspNet.Mvc.Compatibility.DataSource.UseLegacyRouting = true;
        }
        public override void Init() {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, System.EventArgs e) {
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Contains("~/api")) {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }


    }
}
