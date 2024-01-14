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

        var cookie = loginResponse.Headers.GetValues("Set-Cookie").FirstOrDefault();

        _client.DefaultRequestHeaders.Add("Cookie", cookie);

        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        LoginResponseDto? user = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);

        var response = await _client.GetAsync("/api/test-auth");

        Console.WriteLine(response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
