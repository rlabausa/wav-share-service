using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;
using WavShareServiceModels.Constants;

namespace WavShareService.Filters
{
    public class ClientCorrelIdHeaderFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Insert(0, new OpenApiParameter()
            {   Name = Header.ClientCorrelId,
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Example = new OpenApiString("generate"), 
                    Default = new OpenApiString("generate")
                },
                Description = "Use 'generate' if not passing in a unique correlation id",
                AllowEmptyValue = false
            });


        }
    }
}
