using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using portfolio.Models;

[Collection("Sequential")]
public class TestAuth
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task TestProtectedEndpoint()
    {
        // Perform login
        var loginResponse = await _client.PostAsync("/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginAdminDto { Username = "test", Password = "test" }),
            Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        LoginResponseDto? user = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);

        var response = await _client.GetAsync("/api/test-auth");

        Console.WriteLine(response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /*
    [Fact]
    public async Task TestCookieAuth()
    {
        // Perform login using relative URL
        var loginContent = new StringContent(
            JsonConvert.SerializeObject(new LoginAdminDto { Username = "test", Password = "test" }),
            Encoding.UTF8, "application/json");

        var loginResponse = await _client.PostAsync("/api/login-admin", loginContent);
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // Extract the cookie from the login response
        var cookie = loginResponse.Headers.GetValues("Set-Cookie").FirstOrDefault();

        // Test the protected endpoint using relative URL
        // Ensure to include the cookie in the request header
        _client.DefaultRequestHeaders.Add("Cookie", cookie);
        var response = await _client.GetAsync("/api/test-auth");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    */

}
