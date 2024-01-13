using Newtonsoft.Json;
using portfolio.Models;

[Collection("Sequential")]
public class ProjectInfoTests
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task GetProjectInfo()
    {
        var response = await _client.GetAsync("/api/project-info");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<ProjectInfoModel[]>(responseString);

        Assert.IsType<ProjectInfoModel[]>(personalInfo);
        Assert.Equal("test project", personalInfo![0].Name);
        Assert.Equal("test tagline", personalInfo[0].Tagline);
        Assert.Equal("test description", personalInfo[0].Description);
        Assert.Equal("test image", personalInfo[0].Image);
        Assert.Equal("test repo", personalInfo[0].Repo);
        Assert.Equal("test link", personalInfo[0].Link);
        Assert.Equal(["test", "test", "test"], personalInfo[0].TechStack);

        Assert.Equal("test project 2", personalInfo![1].Name);
        Assert.Equal("test tagline 2", personalInfo[1].Tagline);
        Assert.Equal("test description 2", personalInfo[1].Description);
        Assert.Equal("test image 2", personalInfo[1].Image);
        Assert.Equal("test repo 2", personalInfo[1].Repo);
        Assert.Equal("test link 2", personalInfo[1].Link);
        Assert.Equal(["test 2", "test 2", "test 2"], personalInfo[1].TechStack);
    }
}
