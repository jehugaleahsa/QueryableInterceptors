# QueryableInterceptors

Seemlessly interact with IQueryable instances.

Download using NuGet:
    [QueryableInterceptors](http://www.nuget.org/packages/QueryableInterceptors/)
    [ExceptionWrapping](http://www.nuget.org/packages/QueryableInterceptors.ExceptionWrapping/)

## Exception Wrapping Overview
It's common practice to expose an `IQueryable` to the business layer. The business layer is responsible for applying filters, ording, etc. to the `IQueryable` to get at the needed data. However, if an error occurs, the business layer has no idea how to handle an exception coming from the underlying data technology stack (Entity Framework, LINQ to SQL, etc.). The developer has little choice but to handle a generic `Exception` and hope it's a connectivity issue and not a bug.

The `ExceptionWrapping` project aims at allowing developers to wrap data technology stack exceptions with an exception type that the business layer can handle. The following is an example that wraps exceptions coming from Entity Framework:

    using QueryableInterceptors.ExceptionWrapping;
    
    ...
    
    public IQueryable<Customer> GetCustomerQuery()
    {
        return context.DbSet<Customer>()
                      .WrapExceptions()
                      .OfType<EntityException>()
                      .With<DataLayerException>();
    }
    
The `WrapExceptions` method can be called as many times as needed for the different types of exceptions that will be handled. The exceptions are handled in the order they appear in the LINQ expression, so make sure to put more generic exception handlers at the end of the query.

Since the interceptor classes are seemless, you can call `WrapExceptions` anywhere in the LINQ query; however, it is usually better to do it up-front to avoid unhandled exceptions from being thrown by earlier method calls (like `Include`).

There are various overloads of the `With` method to allow more control over how the exceptions are wrapped.

## QueryableInterceptors Overview
It is really, really hard to work with `IQueryable`s without impacting the final output. This project is an attempt to provide a base class that will allow you to safely interact and extract information from the `IQueryable` throughout its lifetime. All wrappers will inherit from the `InterceptingQueryProvider` class. The best way to learn how to use the class is to look at the `WrappedQueryProvider` class in the *ExceptionWrapping* library. 
