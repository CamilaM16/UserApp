using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using user_app.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace user_app
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model:builder.GetEdmModel()
            );
        }
    }
}
