namespace portfolio.Models;

public class ProjectInfoModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Tagline { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Repo { get; set; }
    public string? Link { get; set; }
    public string[]? TechStack { get; set; }
    public string? Type { get; set; }
}

public class CreateProjectDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Tagline { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Repo { get; set; }
    public string? Link { get; set; }
    public string[]? TechStack { get; set; }
    public string? Type { get; set; }
}

public class UpdateProjectDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Tagline { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Repo { get; set; }
    public string? Link { get; set; }
    public string[]? TechStack { get; set; }
    public string? Type { get; set; }
}
