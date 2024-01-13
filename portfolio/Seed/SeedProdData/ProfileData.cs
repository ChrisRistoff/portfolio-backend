namespace portfolio.Seed;

public class ProfileObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public string Github { get; set; }
    public string? Linkedin { get; set; }
    public string Image { get; set; }
}

public class ProfileData
{
    public static ProfileObject GetProfileData()
    {
        return new ProfileObject
        {
            Id = 1,
            Name = "Krasen Hristov",
            Email = "krsnhrstv@gmail.com",
            Bio =
                "My journey in the world of technology is fueled by a strong passion for learning and mastering the intricate aspects of server-side development. I am deeply engaged in understanding the fundamentals of scalable and efficient system design, eager to explore various technologies and methodologies.",
            Github = "www.github.com/krasenHristov",
            Linkedin = null,
            Image = "https://avatars.githubusercontent.com/u/59872801?v=4"
        };
    }
}
