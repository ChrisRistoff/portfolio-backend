using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Repositories;
using portfolio.Services;

namespace portfolio.Controllers;

[ApiController]
public class PersonalInfoController(PersonalInfoRepository personalInfoRepository, StorageService storageService) : ControllerBase
{
    [HttpGet("apis/personal-info")]
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

    [HttpPatch("apis/personal-info/title")]
    [Authorize]
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

    [HttpPatch("apis/personal-info/bio")]
    [Authorize]
    public async Task<IActionResult> UpdateBio(UpdateBioModel model)
    {
        try
        {
            var result = await personalInfoRepository.UpdateBio(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("apis/personal-info/image")]
    [Authorize]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            var result = await storageService.UploadFileAsync(file.OpenReadStream(), file.FileName);

            var updatedPersonalInfo = await personalInfoRepository.UploadImage(result);

            return Ok(updatedPersonalInfo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
