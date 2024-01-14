using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

public class SharedTestResources : IDisposable
{
    public static readonly WebApplicationFactory<Program> Factory = new WebApplicationFactory<Program>();
    public HttpClient Client { get; private set; }

    public SharedTestResources()
    {
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}
