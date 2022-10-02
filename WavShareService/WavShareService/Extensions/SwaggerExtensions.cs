using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WavShareService.Filters;

namespace WavShareService.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add Swagger Generator with required configurations.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>The <see cref="IServiceCollection"/> so additional calls can be chained.</returns>
        public static IServiceCollection AddConfiguredSwaggerGen(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opts =>
            {
                opts.DescribeAllParametersInCamelCase();

                //opts.OperationFilter<ClientCorrelIdHeaderFilter>();

                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WavShare API",
                    Description = "An ASP.NET Core Web API for managing .wav files in the WavShare application",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ruby Labausa",
                        Url = new Uri("https://github.com/rlabausa")
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Example License",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });


                // Enhance Swagger UI with XML descriptions from code
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
