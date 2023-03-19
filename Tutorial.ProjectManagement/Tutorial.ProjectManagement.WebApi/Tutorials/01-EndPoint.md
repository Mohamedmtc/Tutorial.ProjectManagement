# ASP.NET Core API Endpoints

## Introduction

Instead of Model-View-Controller (MVC) the pattern becomes Request-EndPoint-Response(REPR). The REPR (reaper) pattern is much simpler and groups everything that has to do with a particular API endpoint together. It follows SOLID principles, in particular SRP Single responsibility principl and OCP Open-Closed principle

SRP states that a class must have only one reason to change. OCP states that the class must be closed for modification but open to extension

It also has all the benefits of feature folders and better follows the Common Closure Principle by grouping together things that change together.


## 3. Getting Started

I'll look to add detailed documentation in the future but for now here's all you need to get started (you can also check the sample project):

1. Add the [Ardalis.ApiEndpoints NuGet package](https://www.nuget.org/packages/Ardalis.ApiEndpoints/) to your ASP.NET Core web project.
2. Create Endpoint classes by inheriting from either `EndpointBaseSync<TRequest,TResponse>` (for endpoints that accept a model as input) or `EndpointBaseSync<TResponse>` (for endpoints that simply return a response). For example, a POST endpoint that creates a resource and then returns the newly created record would use the version that includes both a Request and a Response. A GET endpoint that just returns a list of records and doesn't accept any arguments would use the second version.
3. Implement the base class's abstract `Handle()` method.
4. Make sure to add a `[HttpGet]` or similar attribute to your `Handle()` method, specifying its route.
5. Define your `TResponse` type in a file in the same folder as its corresponding endpoint (or in the same file if you prefer). 
6. Define your `TRequest` type (if any) just like the `TResponse` class.
7. Test your ASP.NET Core API Endpoint. If you're using Swagger/OpenAPI it should just work with it automatically.

### Adding common endpoint groupings using Swagger

In a standard Web API controller, methods in the same class are grouped together in the Swagger UI. To add this same functionality for endpoints:

1. Install the Swashbuckle.AspNetCore.Annotations
``` bash
dotnet add package Swashbuckle.AspNetCore.Annotations
```
2. Add EnableAnnotations to the Swagger configuration in Startup.cs
``` csharp
services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
});
```
3. Add the following attribute to endpoint methods
``` csharp
[HttpPost("/authors")]
[SwaggerOperation(
    Summary = "Creates a new Author",
    Description = "Creates a new Author",
    OperationId = "Author.Create",
    Tags = new[] { "AuthorEndpoint" })
]
public override async Task<ActionResult<CreateAuthorResult>> HandleAsync([FromBody]CreateAuthorCommand request)
{
    var author = new Author();
    _mapper.Map(request, author);
    await _repository.AddAsync(author);

    var result = _mapper.Map<CreateAuthorResult>(author);
    return Ok(result);
}
```
Option to use service dependency injection instead of constructor
``` csharp
// File: sample/SampleEndpointApp/Endpoints/Authors/List.cs
public class List : BaseAsyncEndpoint
    .WithRequest<AuthorListRequest>
    .WithResponse<IList<AuthorListResult>>
{
    private readonly IAsyncRepository<Author> repository;
    private readonly IMapper mapper;

    public List(
        IAsyncRepository<Author> repository,
        IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    [HttpGet("/authors")]
    [SwaggerOperation(
        Summary = "List all Authors",
        Description = "List all Authors",
        OperationId = "Author.List",
        Tags = new[] { "AuthorEndpoint" })
    ]
    public override async Task<ActionResult<IList<AuthorListResult>>> HandleAsync(

        [FromQuery] AuthorListRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.PerPage == 0)
        {
            request.PerPage = 10;
        }
        if (request.Page == 0)
        {
            request.Page = 1;
        }
        var result = (await repository.ListAllAsync(request.PerPage, request.Page, cancellationToken))
            .Select(i => mapper.Map<AuthorListResult>(i));

        return Ok(result);
    }
}
```
