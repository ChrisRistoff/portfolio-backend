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
        var response = await _client.GetAsync("/apis/project-info");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var personalInfo = JsonConvert.DeserializeObject<ProjectInfoModel[]>(responseString);

        Assert.IsType<ProjectInfoModel[]>(personalInfo);
        Assert.Equal("test project", personalInfo![3].Name);
        Assert.Equal("test tagline", personalInfo[3].Tagline);
        Assert.Equal("test description", personalInfo[3].Description);
        Assert.Equal("test image", personalInfo[3].Image);
        Assert.Equal("test repo", personalInfo[3].Repo);
        Assert.Equal("test link", personalInfo[3].Link);
        Assert.Equal(["test", "test", "test"], personalInfo[3].TechStack);

        Assert.Equal("test project 2", personalInfo![2].Name);
        Assert.Equal("test tagline 2", personalInfo[2].Tagline);
        Assert.Equal("test description 2", personalInfo[2].Description);
        Assert.Equal("test image 2", personalInfo[2].Image);
        Assert.Equal("test repo 2", personalInfo[2].Repo);
        Assert.Equal("test link 2", personalInfo[2].Link);
        Assert.Equal(["test 2", "test 2", "test 2"], personalInfo[2].TechStack);
    }

    [Fact]
    public async Task CreateProject_ShouldSucceed()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/apis/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var project = new CreateProjectDto
        {
            Id = 3,
            Name = "test project 5",
            Tagline = "test tagline 5",
            Description = "test description 5",
            Image = "test image 5",
            Repo = "test repo 5",
            Link = "test link 5",
            TechStack = ["test 5", "test 5", "test 5"],
            Type = "test type 5"
        };

        var response2 = await _client.PostAsync("/apis/project-info", new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json"));

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
        var response = await _client.GetAsync("/apis/project-info/1");
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

    [Fact]
    public async Task GetProjectById_ShouldFail()
    {
        var response = await _client.GetAsync("/apis/project-info/100");
        await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProject_ShouldSucceed()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/apis/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var project = new UpdateProjectDto
        {
            Id = 1,
            Name = "test project 5",
            Tagline = "test tagline 5",
            Description = "test description 5",
            Image = "test image 5",
            Repo = "test repo 5",
            Link = "test link 5",
            TechStack = ["test 5", "test 5", "test 5"],
            Type = "test type 5"
        };

        var response2 = await _client.PatchAsync("/apis/project-info/3", new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json"));

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
    public async Task UpdateProject_ShouldFail()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/apis/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var project = new UpdateProjectDto
        {
            Id = 1,
            Name = "test project 5",
            Tagline = "test tagline 5",
            Description = "test description 5",
            Image = "test image 5",
            Repo = "test repo 5",
            Link = "test link 5",
            TechStack = ["test 5", "test 5", "test 5"],
            Type = "test type 5"
        };

        var response2 = await _client.PatchAsync("/apis/project-info/100", new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task DeleteProject_ShouldSucceed()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/apis/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var responseDelete = await _client.DeleteAsync("/apis/project-info/4");

        Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);

        var responseGetDeletedArticle = await _client.GetAsync("/apis/project-info/4");
        Assert.Equal(HttpStatusCode.NotFound, responseGetDeletedArticle.StatusCode);
    }

    [Fact]
    public async Task DeleteProject_ShouldFail()
    {
        var user = new LoginAdminDto
        {
            Username = "test",
            Password = "test"
        };

        var response = await _client.PostAsync("/apis/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(responseString);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);

        var responseDelete = await _client.DeleteAsync("/apis/project-info/100");

        Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);
    }
}
