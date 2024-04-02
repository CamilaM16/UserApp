using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using user_app.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System.Web.Http.Cors;

namespace user_app
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
            config.EnableCors(cors);
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users")
                .EntityType.Select().Filter().OrderBy().Expand().Count();
            
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model:builder.GetEdmModel()
            );
        }
    }
}
