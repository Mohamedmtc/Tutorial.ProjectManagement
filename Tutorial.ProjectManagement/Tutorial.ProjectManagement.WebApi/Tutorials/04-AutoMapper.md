# AutoMapper
## Why use API Versioning?

The primary reason we would offer API versioning is that the same method is able to be called in the same way, but with a different version number in the URL. This gives us the option to serve the data to clients that we would like, but our customers are also able to wait and update their applications when they got time for it in their systems. Sounds good? Let’s see how you can achieve just that in your .NET Core Web API.

## Implementing API Versioning in .NET Core Web API using .NET 6

For this tutorial I will be using .NET 6, please make sure you have that installed on your computer. You also have to install the following package:

    Install-Package AutoMapper 

    Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection


The first thing you have to do is go to your program.cs file and add the following code to the services section:



The ReportAPIVersions flag is optional, but it can be useful. The function of that one is that it allows for the API to return versions in the response header. When a client is calling your API they will see a flag with available options for that method.

## Update controllers to accept API versioning

    public static void AddMapper(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WebApiDependencyInjection).Assembly)).CreateMapper();
            services.AddSingleton(m => mapper);

        }

##  Integrate .NET Core API Versioning with Swagger

add api version Files

    ConfigureSwaggerOptions.cs
    SwaggerDefaultValues.cs


modify the launchSettings.json to add "api" startup location


    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    builder.Services.AddSwaggerGen(
                options =>
                {
                    options.EnableAnnotations();
                    options.OperationFilter<SwaggerDefaultValues>();
                });

    builder.Services.AddApiVersioning(o =>  { o.AssumeDefaultVersionWhenUnspecified =   true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new MediaTypeApiVersionReader("ver"));
    });


    builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    if (app.Environment.IsDevelopment())
    {
    string basePath = "api";
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger(c => { c.RouteTemplate = basePath + "/swagger/{documentName}/swagger.json"; });
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = $"{basePath}/swagger";
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/{basePath}/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
    }
    `

to program.cs