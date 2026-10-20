using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplicationConsume.Controllers;
using WebApplicationConsume.Models;
using Xunit;

public sealed class MultipartTests
{
    [Fact]
    public async Task Category_fields_are_outbound_plain_text_not_an_HTML_response()
    {
        const string payload = "<img src=x onerror=alert(1)>";
        using var handler = new RecordingHandler();
        using var client = new HttpClient(handler);
        using var controller = new CategoryController(new ClientFactory(client));
        var result = await controller.Create(new CategoryCreateViewModel
        {
            Name = payload, Description = payload, File = null!
        });
        Assert.IsType<ViewResult>(result);
        Assert.Equal("http://localhost:5180/api/Category", handler.Address);
        Assert.Equal(new[] { "text/plain", "text/plain" }, handler.Types);
        Assert.Equal(new[] { payload, payload }, handler.Values);
    }

    private sealed class ClientFactory(HttpClient client) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => client;
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public string? Address { get; private set; }
        public List<string?> Types { get; } = [];
        public List<string> Values { get; } = [];
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            Address = request.RequestUri!.AbsoluteUri;
            foreach (var part in Assert.IsType<MultipartFormDataContent>(request.Content))
            {
                Types.Add(part.Headers.ContentType?.MediaType);
                Values.Add(await part.ReadAsStringAsync(token));
            }
            return new HttpResponseMessage(HttpStatusCode.Conflict);
        }
    }
}
