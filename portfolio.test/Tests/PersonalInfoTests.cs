using Newtonsoft.Json;
using portfolio.Models;

[Collection("Sequential")]
public class PersonalInfoTests
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task GetPersonalInfo()
    {
        var response = await _client.GetAsync("/api/personal-info");
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
}
