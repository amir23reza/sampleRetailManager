using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace dataManager.App_Start
{
    /* adding an optional authorization field to be able to use restricted endpoint which require the access token in swagger */
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                required = false,
                type = "string",
                description = "access token"
            });
        }
    }
}