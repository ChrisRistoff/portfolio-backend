namespace portfolio.Seed;

public class ProfileTestData
{
    public static ProfileObject GetProfileTestData()
    {
        return new ProfileObject
        {
            Name = "test name",
            Title = "test title",
            Email = "test email",
            Bio = "test bio",
            Github = "test github",
            Linkedin = "test linkedin",
            Image = "test image"
        };
    }
}
