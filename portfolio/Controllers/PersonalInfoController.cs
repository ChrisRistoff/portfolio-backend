using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class PersonalInfoController(PersonalInfoRepository personalInfoRepository) : ControllerBase
{
    [HttpGet("api/personal-info")]
    public async Task<ActionResult<PersonalInfoModel>> GetPersonalInfo()
    {
        try
        {
            PersonalInfoModel result = await personalInfoRepository.GetPersonalInfo();
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("api/personal-info/title")]
    public async Task<IActionResult> UpdateTitle(UpdateTitleModel model)
    {
        try
        {
            var result = await personalInfoRepository.UpdateTitle(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
