using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Tutorial.ProjectManagement.Application.Contracts;
using Tutorial.ProjectManagement.Application.DependencyInjection;
using Tutorial.ProjectManagement.WebApi.DependencyInjection;
using Tutorial.ProjectManagement.WebApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWebApilayerDependencyInjection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(
                options =>
                {
                    options.EnableAnnotations();
                    options.OperationFilter<SwaggerDefaultValues>();
                });

builder.Services.AddApiVersioning(o =>
{

    o.AssumeDefaultVersionWhenUnspecified = true;
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

var app = builder.Build();

// Configure the HTTP request pipeline.

builder.Services.AddEndpointsApiExplorer();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
