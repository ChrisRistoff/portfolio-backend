using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using portfolio.Models;

namespace portfolio.test.Tests;

[Collection("Sequential")]
public class PersonalInfoTests
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task GetPersonalInfo()
    {
        var response = await _client.GetAsync("/apis/personal-info");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<PersonalInfoModel>(responseString);

        Assert.IsType<PersonalInfoModel>(personalInfo);
        Assert.Equal("test name", personalInfo!.Name);
        Assert.Equal("test email", personalInfo.Email);
        Assert.Equal("test bio", personalInfo.Bio);
        Assert.Equal("test github", personalInfo.Github);
        Assert.Equal("test linkedin", personalInfo.Linkedin);
        Assert.Equal("test image", personalInfo.Image);
    }

    [Fact]
    public async Task UpdateTitle()
    {
        // login user
        var loginModel = new LoginAdminDto { Username = "test", Password = "test" };

        // login
        var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

        var loginResponse = await _client.PostAsync("/apis/login-admin", loginContent);
        loginResponse.EnsureSuccessStatusCode();
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
        var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // set token
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponseDto!.Token);

        var model = new UpdateTitleModel { Title = "test title" };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _client.PatchAsync("/apis/personal-info/title", content);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<PersonalInfoModel>(responseString);

        Assert.IsType<PersonalInfoModel>(personalInfo);
        Assert.Equal("test name", personalInfo!.Name);
        Assert.Equal("test email", personalInfo.Email);
        Assert.Equal("test bio", personalInfo.Bio);
        Assert.Equal("test github", personalInfo.Github);
        Assert.Equal("test linkedin", personalInfo.Linkedin);
        Assert.Equal("test image", personalInfo.Image);
        Assert.Equal("test title", personalInfo.Title);
    }

    [Fact]
    public async Task UpdateBio()
    {
        // login user
        var loginModel = new LoginAdminDto { Username = "test", Password = "test" };

        // login
        var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

        var loginResponse = await _client.PostAsync("/apis/login-admin", loginContent);
        loginResponse.EnsureSuccessStatusCode();
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
        var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // set token
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponseDto!.Token);

        var model = new UpdateBioModel { Bio = "test bio" };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _client.PatchAsync("/apis/personal-info/bio", content);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<PersonalInfoModel>(responseString);

        Assert.IsType<PersonalInfoModel>(personalInfo);
        Assert.Equal("test name", personalInfo!.Name);
        Assert.Equal("test email", personalInfo.Email);
        Assert.Equal("test bio", personalInfo.Bio);
        Assert.Equal("test github", personalInfo.Github);
        Assert.Equal("test linkedin", personalInfo.Linkedin);
        Assert.Equal("test image", personalInfo.Image);
        Assert.Equal("test title", personalInfo.Title);
    }
}
