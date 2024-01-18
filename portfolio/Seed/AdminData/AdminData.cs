using portfolio.Models;

namespace portfolio.Seed;

public class AdminData
{
    public static Admin[] GetAdminData()
    {
        return new Admin[]
        {
            new Admin
            {
                Username = "test",
                Password = "test",
                Role = "test"
            }
        };
    }
}
