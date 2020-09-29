using System.Web.Http;
using WebActivatorEx;
using MoviesProjectStandard;
using Swashbuckle.Application;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MoviesProjectStandard
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                       
                        c.SingleApiVersion("v1", "MoviesProjectStandard");
                        c.ApiKey("XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==")
                        .Description("Filling bearer token here")
                        .Name("Authorization")
                        .In("header");

                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
