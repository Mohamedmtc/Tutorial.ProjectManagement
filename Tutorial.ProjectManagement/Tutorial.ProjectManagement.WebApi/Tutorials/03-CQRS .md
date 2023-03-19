# CQRS

CQRS stands for Command and Query Responsibility Segregation and uses to separate read(queries) and write(commands).

large application 90% read and 10% write

As we know, in our application we mostly use a single data model to read and write data, which will work fine and perform CRUD operations easily. But, when the application becomes a vast in that case, our queries return different types of data as an object so that become hard to manage with different DTO objects. Also, the same model is used to perform a write operation. As a result, the model becomes complex

CQRS helps to decouple operations and make the application more scalable and flexible on large scale.

## MediatR

  MediatR pattern helps to reduce direct dependency between multiple objects and make them collaborative through MediatR.

  In .NET Core MediatR provides classes that help to communicate with multiple objects efficiently in a loosely coupled manner.

  ### Install

    Install-Package MediatR


## MediatR Functions

MediatR has two kinds of messages it dispatches:

1. Request/response messages, dispatched to a single handler
    * Send may return a response, but do not have to do it.
2. Notification messages, dispatched to multiple handlers
    * Publish never return the result.



## Add toDependency injection

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IMediatorImplementor).Assembly));

