namespace portfolio.Seed;

public class ProjectObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tagline { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Repo { get; set; }
    public string Link { get; set; }
    public string[] TechStack { get; set; }
    public string? Type { get; set; }
}

public class ProjectData
{
    public static ProjectObject[] GetProjectData()
    {
        return new ProjectObject[]
        {
            new ProjectObject
            {
                Id = 1,
                Name = "Portfolio",
                Tagline = "My personal website",
                Description = "My personal website, built with ASP.NET Core 8 and C#.",
                Image = "https://i.imgur.com/6Z2Q9ZM.png",
                Repo = "www.github.com/krasenHristov/portfolio",
                Link = "www.krasenhristov.com",
                TechStack = new string[] { "C#", "ASP.NET Core", "PostgreSQL", "Docker", "Nginx" },
                Type = "Backend"
            },
        };
    }
}
