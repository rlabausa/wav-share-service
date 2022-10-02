using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.Constants;

namespace WavShareService.Filters
{
    /// <summary>
    /// Filters for default required client-correl-id header.
    /// </summary>
    [Obsolete($"{nameof(ClientCorrelIdHeaderFilter)} is deprecated.  Please apply required header filters through the {nameof(ApiRequestHeaders)} class", true)]
    public class ClientCorrelIdHeaderFilter: IOperationFilter
    {
        /// <summary>
        /// Add client-correl-id header if none are present.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
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
