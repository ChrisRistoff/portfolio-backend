using portfolio.Models;

namespace portfolio.Seed;

public class TestAdminData
{
    public Admin[] GetTestAdminData()
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
