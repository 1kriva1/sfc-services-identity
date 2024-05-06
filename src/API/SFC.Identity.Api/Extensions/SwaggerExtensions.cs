﻿using System.Reflection;

using SFC.Identity.Application;

namespace SFC.Identity.Api.Extensions;

public static class SwaggerExtensions
{
    private const string SPECIFICATION_NAME = "common";
    private const string TITLE = "SFC.Identity";

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc(SPECIFICATION_NAME, new()
            {
                Title = TITLE,
                Version = "1",
                Description = "Through this API you can register, login, logout, check existence of users and handle JWT tokens.",
                Contact = new()
                {
                    Email = "krivorukandrey@gmail.com",
                    Name = "Andrii Kryvoruk"
                }
            });

            // controller comments
            setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

            // request/response and models comments
            setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{typeof(ApplicationRegistration).Assembly.GetName().Name}.xml"));
        });
    }

    public static void UseSwagger(this IApplicationBuilder builder)
    {
        SwaggerBuilderExtensions.UseSwagger(builder);
        builder.UseSwaggerUI(setupAction =>
        {
            setupAction.SwaggerEndpoint($"/swagger/{SPECIFICATION_NAME}/swagger.json", TITLE);
        });
    }
}
