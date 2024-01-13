namespace portfolio.Seed;

public class ProjectTestData
{
    public static ProjectObject[] GetProjectData()
    {
        return new ProjectObject[]
        {
            new ProjectObject
            {
                Name = "test project",
                Tagline = "test tagline",
                Description = "test description",
                Image = "test image",
                Repo = "test repo",
                Link = "test link",
                TechStack = new string[] { "test", "test", "test" },
                Type = ProjectObject.ProjectType.Frontend,
            },

            new ProjectObject
            {
                Name = "test project 2",
                Tagline = "test tagline 2",
                Description = "test description 2",
                Image = "test image 2",
                Repo = "test repo 2",
                Link = "test link 2",
                TechStack = new string[] { "test 2", "test 2", "test 2" },
                Type = ProjectObject.ProjectType.Backend,
            },
        };
    }
}
