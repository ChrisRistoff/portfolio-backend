namespace portfolio.Models;

public class PersonalInfoModel
{
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public string? Github { get; set; }
    public string? Linkedin { get; set; }
    public string? Image { get; set; }
}

public class UpdateTitleModel
{
    public string? Title { get; set; }
}

public class UpdateBioModel
{
    public string? Bio { get; set; }
}
