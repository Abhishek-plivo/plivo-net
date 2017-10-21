# Plivo DotNet Helper SDK

**Note**: `Bleeding edge: Use with caution`

## About

__Plivo C#.NET Helper Library__ simplifies the process of making [Plivo API](http://plivo.com/docs/api/) calls and generating [Plivo XML](http://plivo.com/docs/xml/).

## Getting Started

### Installation

This helper library can be installed / used in two ways,

1. From Source.
2. From the [NuGet Package Manager](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c) (recommended).

#### From Source

To use this library from source follow the instructions below, 

+ Get the source from Github using `git clone` or using the [download link](https://github.com/plivo/plivo-dotnet/archive/master.zip).
+ Open the solution file in Visual Studio.
+ Install [NuGet Package Manager](http://docs.nuget.org/docs/start-here/installing-nuget).
+ Go to `Tools -> Library Package Manager -> Packager Manager Console`.
+ Enter the following commands in the console to install [RestSharp](https://www.nuget.org/packages/RestSharp) (a Simple REST and HTTP API Client) and [Json.NET](https://www.nuget.org/packages/newtonsoft.json/) (a popular high-performance JSON framework for .NET),  
```
PM> Install-Package RestSharp -Version 105.2.3
PM> Install-Package Newtonsoft.Json -Version 10.0.2
PM> Install-Package NUnit -Version 3.7.1
```
+ Once the RestSharp and Json.NET packages are successfully installed, close the console. Right Click on the solution (in this case Plivo) in the _Solution Explorer_ and click _Clean_ and then _Build_.
+ Next, to start using it in your project, Right Click on your Project Name in the _Solution Explorer_ and click _Add Reference_. In the dialog box which appears, go to the _Browse_ tab and Navigate to `plivo-dotnet\Plivo\bin\Debug` and select the `Plivo.dll` and click OK.
+ Now, you can use Plivo in your code! 

#### From NuGet Package Manager

+ Install [NuGet Package Manager](http://docs.nuget.org/docs/start-here/installing-nuget).
+ Go to `Tools -> Library Package Manager -> Packager Manager Console`.
+ RestSharp and Json.NET are automatically installed as a dependency.

```
PM> Install-Package Plivo
```

If you already have RestSharp or Json.NET installed, its best to uninstall it before installing the specific version.

```
PM> Uninstall-Package RestSharp -Force
PM> Uninstall-Package Newtonsoft.Json -Force
PM> Install-Package Plivo
```

### Authentication:

To use the DotNet SDK with a single client, create an api object with var api = new PlivoApi()`, and all API calls will use this global client by default.
We recommend that you store your credentials in the `PLIVO_AUTH_ID` and the `PLIVO_AUTH_TOKEN` environment variables, so as to avoid the possibility of accidentally committing them to source control. If you do this, you can initialise the client with no arguments and it will automatically fetch them from the environment variables:

```dotnet
var api = new PlivoApi();
```

Alternatively, you can provide these to `PlivoApi()`'s constructor yourself:

```dotnet
var api = new PlivoApi("YOU_AUTH_ID", "YOUR_AUTH_TOKEN");
```

If you are making several requests to Plivo's API, please re-use the same client instance for maximum efficiency.

## The Basics

To send a message:

```dotnet
internal class Program
{
    public static void Main(string[] args)
    {
        var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
        try
        {
            var response = api.Message.Create(
                src:"14153336666",
                dst:new List<String>{"14156667777"},
                text:"Test Message"
            );
            Console.WriteLine(response);
        }
        catch (PlivoRestException e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}
```

To make a call

```dotnet
internal class Program
{
    public static void Main(string[] args)
    {
        var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
        try
        {
            var response = api.Call.Create(
                to:new List<String>{"3213213213"},
                from:"1231231231",
                answerMethod:"GET",
                answerUrl:"http://s3.amazonaws.com/static.plivo.com/answer.xml"
            );
            Console.WriteLine(response);
        }
        catch (PlivoRestException e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}
```

To list all objects of any resource, simply use the request object itself as an iterable:

```dotnet
internal class Program
{
    public static void Main(string[] args)
    {
        var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
        try
        {
            var response = api.Message.List(
                limit:5,
                offset:0
            );
            foreach (var message in response) {
                Console.Writeline(message.Id);
            }
        }
        catch (PlivoRestException e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}
```

To generate PlivoXML:

```dotnet
class MainClass
{
    public static void Main(string[] args)
    {
        Plivo.XML.Response response = new Plivo.XML.Response();
        response.AddSpeak("Hello World, from Plivo!",
                          new Dictionary<string, string>() { });
        Console.WriteLine(response.ToString());
    }
}
```