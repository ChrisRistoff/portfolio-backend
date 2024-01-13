namespace portfolio.Repositories;

public class PersonalInfoRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public PersonalInfoRepository(IConfiguration config)
    {
        _config = config;
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Development")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        if (env == "Production")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
    }
}
