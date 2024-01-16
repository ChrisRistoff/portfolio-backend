namespace portfolio.Seed;

public class ProjectTestData
{
    public static ProjectObject[] GetProjectTestData()
    {
        return new ProjectObject[]
        {
            new ProjectObject
            {
                Id = 1,
                Name = "test project",
                Tagline = "test tagline",
                Description = "test description",
                Image = "test image",
                Repo = "test repo",
                Link = "test link",
                TechStack = new string[] { "test", "test", "test" },
                Type = "Backend"
            },

            new ProjectObject
            {
                Id = 2,
                Name = "test project 2",
                Tagline = "test tagline 2",
                Description = "test description 2",
                Image = "test image 2",
                Repo = "test repo 2",
                Link = "test link 2",
                TechStack = new string[] { "test 2", "test 2", "test 2" },
                Type = "Frontend"
            },

            new ProjectObject
            {
                Id = 3,
                Name = "test project 3",
                Tagline = "test tagline 3",
                Description = "test description 3",
                Image = "test image 3",
                Repo = "test repo 3",
                Link = "test link 3",
                TechStack = new string[] { "test 3", "test 3", "test 3" },
                Type = "Fullstack"
            },

            new ProjectObject
            {
                Id = 4,
                Name = "test project 4",
                Tagline = "test tagline 4",
                Description = "test description 4",
                Image = "test image 4",
                Repo = "test repo 4",
                Link = "test link 4",
                TechStack = new string[] { "test 4", "test 4", "test 4" },
                Type = "Backend"
            },
        };
    }
}
