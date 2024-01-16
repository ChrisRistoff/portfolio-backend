namespace portfolio.Seed;

public class ProjectObject
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
                Description = "A portfolio project I created to easily update project links without the need to revise my entire CV. " +
                              "As I'm still relatively new to deployment and CI/CD pipelines, " +
                              "this approach allows for flexibility and efficiency in managing project references. \n\n" +
                              "Currently working on adding an admin panel where I can edit everything, this will also include a " +
                              "node project inside my portfolio project that will allow me to pull latest information from the DB and update the production seed files.",
                Image = "null",
                Repo = "https://www.github.com/krasenHristov/portfolio-backend",
                Link = "http://ec2-35-179-90-244.eu-west-2.compute.amazonaws.com:8080/swagger/index.html",
                TechStack = new string[] { "C#", "ASP.NET Core 8", "Dapper", "FluentMigrator", "PostgreSQL", "Docker", "GithubActions", "AWS", "JWT" },
                Type = "Backend"
            },

            new ProjectObject
            {
                Id = 2,
                Name = "Portfolio Frontend",
                Tagline = "My personal website",
                Description = "The frontend of my portfolio project has a simple and elegant design that helps me display my projects clearly and without unnecessary complexity. " +
                              "I created this space primarily to present my work and make it easy to update when necessary. " +
                              "This way, I can keep my project references current without having to make extensive changes to my CV.",
                Image = "null",
                Repo = "https://www.github.com/krasenHristov/portfolio-frontend",
                Link = "http://ec2-35-179-90-244.eu-west-2.compute.amazonaws.com:9090",
                TechStack = new string[] { "Typescript", "React", "Vite", "Docker", "GithubActions", "AWS" },
                Type = "Frontend"
            },

            new ProjectObject
            {
                Id = 3,
                Name = "Recipe GitHub",
                Tagline = "Group project, reference Actions and Commits to get the full picture",
                Description = "A Recipe website but with a twist, users are not only able to create their recipes," +
                              "they can also Fork(copy) other users recipes and make their own version of it. " +
                              "There is support for 1-5 star reviws, comments, search, featured recipes using the TSVECTOR table for the search, " +
                              "likes on comments and much more.",
                Image = "null",
                Repo = "https://www.github.com/krasenHristov/RecipeHub",
                Link = "http://ec2-18-130-212-68.eu-west-2.compute.amazonaws.com:5000/swagger/index.html",
                TechStack = new string[] { "C#", "ASP.NET Core 8", "Dapper", "FluentMigrator", "PostgreSQL", "Docker", "GithubActions", "AWS", "JWT", "Firebase Cloud Storage" },
                Type = "Backend"
            },


            new ProjectObject
            {
                Id = 4,
                Name = "Recipe GitHub Frontend",
                Tagline = "Frontend of the Recipe GitHub project",
                Description = "The frontend of this project complements the backend, offering a user-friendly interface for Recipe GitHub. " +
                              "While my primary focus was on setting up CICD and the AWS server, I also provided occasional support to colleagues facing challenges. " +
                              "Continuous communication was essential to ensure seamless integration between frontend and backend components.",
                Image = "null",
                Repo = "https://github.com/gcpearse/RecipeHub-FE",
                Link = "http://ec2-18-130-212-68.eu-west-2.compute.amazonaws.com:3000",
                TechStack = new string[] { "React", "TypeScript", "Redux", "ReduxJS Toolkit", "Docker", "GithubActions", "AWS" },
                Type = "Frontend"
            },


            new ProjectObject
            {
                Id = 5,
                Name = "NC-News Backend",
                Tagline = "A Reddit-like news site",
                Description = "NC-News Backend is a Reddit-like news site where I leveraged technologies beyond my bootcamp curriculum. " +
                              "I not only completed core tasks promptly but also implemented features such as authentication, CICD pipeline, " +
                              "AWS deployment, Docker containerization, TypeScript migration, interactive Swagger documentation, and more. ",
                Image = "null",
                Repo = "https://github.com/krasenHristov/nc-news",
                Link = "http://ec2-35-179-90-244.eu-west-2.compute.amazonaws.com/api/docs",
                TechStack = new string[] { "Node", "Express", "Typescript", "PostgreSQL", "Swagger", "JWT", "Docker", "Jest", "GithubActions", "AWS" },
                Type = "Backend"
            },

            new ProjectObject
            {
                Id = 6,
                Name = "NC-News Frontend",
                Tagline = "Frontend of the NC-News project",
                Description = "The NC-News Frontend complements the backend, providing a functional user interface for the NC-News project. " +
                              "My primary goal was to gain a deep understanding of frontend-backend integration rather than focusing on aesthetics. " +
                              "While it may not be the most visually appealing, it serves as a valuable learning experience for improving my backend development skills.",
                Image = "null",
                Repo = "https://github.com/krasenHristov/nc-news-frontend",
                Link = "http://ec2-35-179-90-244.eu-west-2.compute.amazonaws.com:3000",
                TechStack = new string[] { "React", "Javascript", "Docker", "GithubActions", "AWS" },
                Type = "Frontend"
            },

            new ProjectObject
            {
                Id = 7,
                Name = "Simple Recipe App",
                Tagline = "Simple recipe app",
                Description = "I created the Simple Recipe App as a learning exercise mostly by following a tutorial. " +
                              "This project allowed me to gain hands-on experience with Python, Django, PostgreSQL, Docker, Github Actions, and AWS deployment. " +
                              "While the initial implementation was based on the tutorial, " +
                              "I also took the opportunity to customize and extend the project to further enhance my understanding of these technologies.",
                Image = "null",
                Repo = "https://github.com/krasenHristov/django",
                Link = "http://ec2-54-90-144-115.compute-1.amazonaws.com/api/docs/",
                TechStack = new string[] { "Python", "Django", "PostgreSQL", "Docker", "GithubActions", "AWS" },
                Type = "Backend"
            },
        };
    }
}
