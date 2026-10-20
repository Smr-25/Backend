using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public sealed class AntiforgeryTests
{
    [Fact]
    public async Task Login_rejects_missing_and_forged_tokens_but_accepts_the_issued_token()
    {
        await using var factory = new WebApplicationFactory<WebApplicationConsume.Program>();
        using var client = factory.CreateClient();
        var fields = new Dictionary<string, string> { ["Email"] = "invalid", ["Password"] = "" };
        using var missing = await client.PostAsync("/UiAccount/Login", new FormUrlEncodedContent(fields));
        Assert.Equal(HttpStatusCode.BadRequest, missing.StatusCode);
        fields["__RequestVerificationToken"] = "forged";
        using var forged = await client.PostAsync("/UiAccount/Login", new FormUrlEncodedContent(fields));
        Assert.Equal(HttpStatusCode.BadRequest, forged.StatusCode);
        var form = await client.GetStringAsync("/UiAccount/Login");
        var token = Regex.Match(form, "name=\"__RequestVerificationToken\"[^>]*value=\"([^\"]+)\"");
        Assert.True(token.Success);
        fields["__RequestVerificationToken"] = WebUtility.HtmlDecode(token.Groups[1].Value);
        using var accepted = await client.PostAsync("/UiAccount/Login", new FormUrlEncodedContent(fields));
        Assert.Equal(HttpStatusCode.OK, accepted.StatusCode);
    }
}
