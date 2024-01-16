using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using portfolio.Models;

namespace portfolio.test.Tests;

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

    [Fact]
    public async Task CreateProject_ShouldSucceed()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var project = new CreateProjectDto
        {
            Id = 3,
            Name = "test project 3",
            Tagline = "test tagline 3",
            Description = "test description 3",
            Image = "test image 3",
            Repo = "test repo 3",
            Link = "test link 3",
            TechStack = ["test 3", "test 3", "test 3"],
            Type = "test type 3"
        };

        var response2 = await _client.PostAsync("/api/project-info", new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json"));

        response2.EnsureSuccessStatusCode();

        var responseString2 = await response2.Content.ReadAsStringAsync();

        var projectInfo = JsonConvert.DeserializeObject<ProjectInfoModel>(responseString2);

        Assert.Equal(projectInfo?.Id, project.Id);
        Assert.Equal(projectInfo?.Name, project.Name);
        Assert.Equal(projectInfo?.Tagline, project.Tagline);
        Assert.Equal(projectInfo?.Description, project.Description);
        Assert.Equal(projectInfo?.Image, project.Image);
        Assert.Equal(projectInfo?.Repo, project.Repo);
        Assert.Equal(projectInfo?.Link, project.Link);
        Assert.Equal(projectInfo?.Type, project.Type);
    }

    [Fact]
    public async Task GetProjectById_ShouldSucceed()
    {
        var response = await _client.GetAsync("/api/project-info/1");
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<ProjectInfoModel>(responseString);

        Assert.IsType<ProjectInfoModel>(personalInfo);
        Assert.Equal("test project", personalInfo!.Name);
        Assert.Equal("test tagline", personalInfo.Tagline);
        Assert.Equal("test description", personalInfo.Description);
        Assert.Equal("test image", personalInfo.Image);
        Assert.Equal("test repo", personalInfo.Repo);
        Assert.Equal("test link", personalInfo.Link);
        Assert.Equal(["test", "test", "test"], personalInfo.TechStack);
    }
}
