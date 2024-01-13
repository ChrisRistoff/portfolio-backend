using Microsoft.AspNetCore.Mvc;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class PersonalInfoController(PersonalInfoRepository personalInfoRepository) : ControllerBase
{
    [HttpGet("api/personal-info")]
    public async Task<IActionResult> GetPersonalInfo()
    {
        var result = await personalInfoRepository.GetPersonalInfo();
        return Ok(result);
    }
}
