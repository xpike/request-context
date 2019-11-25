# XPike.RequestContext

[![Build Status](https://dev.azure.com/xpike/xpike/_apis/build/status/xpike.requestcontext?branchName=master)](https://dev.azure.com/xpike/xpike/_build/latest?definitionId=13&branchName=master)
![Nuget](https://img.shields.io/nuget/v/XPike.RequestContext)

Provides a protocol-agnostic abstraction around the concept of a Request Context.

## Overview

Intended to provide a common surface area to program against when developing components
that may be invoked over multiple communication methods (eg: over a REST API via HTTP and
through a RabbitMQ topic via AMQP), such as to access a Tenant ID or Request Trace ID header.

Allows for a list of Providers to be registered to support multiple protocols within the
same process (eg: a background AMQP worker within an HTTP API).

Currently supports:
- .NET Framework: ASP.NET WebApi/MVC
- .NET Core: ASP.NET Core

## Usage

**In ASP.NET Core using Microsoft DI:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddXPikeHttpRequestContext()
            .AddXPikeDependencyInjection();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseXPikeDependencyInjection();
}
```

**In ASP.NET Core using SimpleInjector:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddXPikeDependencyInjection()
            .LoadPackage(new XPike.RequestContext.Http.AspNetCore.Package());
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseXPikeDependencyInjection();
}
```

**In .NET Framework:**

```csharp
IDependencyCollection.LoadPackage(new XPike.RequestContext.Http.WebApi.Package());
```

## Exported Services

### XPike.RequestContext

##### Scoped

- **`IRequestContext`**  
  Injects the current context via `IRequestContextAccessor.RequestContext`.

##### Singleton

- **`IRequestContextAccessor`**  
  **=> `RequestContextAccessor`**

- **`IDefaultRequestContextProvider`**  
  **=> `DefaultRequestContextProvider`**

##### Singleton Collection

- **`IRequestContextProvider`**  
  **Add: `IDefaultRequestContextProvider`**

### XPike.RequestContext.Http

##### Singleton Collection

- **`IRequestContextProvider`**  
  **Add: `IHttpRequestContextProvider`**

### XPike.RequestContext.Http.AspNetCore

##### Singleton

- **`IAspNetCoreRequestContextProvider`**  
  **=> `AspNetCoreRequestContextProvider`**

- **`IHttpRequestContextProvider`**  
  **=> `IAspNetCoreRequestContextProvider`**

##### XPike.RequestContext.Http.WebApi

##### Singleton

- **`IWebApiCoreRequestContextProvider`**  
  **=> `WebApiCoreRequestContextProvider`**

- **`IHttpRequestContextProvider`**  
  **=> `IWebApiCoreRequestContextProvider`**

## Dependencies

##### XPike.RequestContext

- **`XPike.IoC`**
- **`XPike.Logging`**
- `XPike.Settings`
- `XPike.Configuration`

## Building and Testing

Building from source and running unit tests requires a Windows machine with:

* .Net Core 3.0 SDK
* .Net Framework 4.6.1 Developer Pack

## Issues

Issues are tracked on [GitHub](https://github.com/xpike/request-context/issues). Anyone is welcome to file a bug,
an enhancement request, or ask a general question. We ask that bug reports include:

1. A detailed description of the problem
2. Steps to reproduce
3. Expected results
4. Actual results
5. Version of the package xPike
6. Version of the .Net runtime

## Contributing

See our [contributing guidelines](https://github.com/xpike/documentation/blob/master/docfx_project/articles/contributing.md)
in our documentation for information on how to contribute to xPike.

## License

xPike is licensed under the [MIT License](LICENSE).
