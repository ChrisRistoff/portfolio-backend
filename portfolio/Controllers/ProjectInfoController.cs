using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class ProjectInfoController(ProjectInfoRepository projectInfoRepository) : ControllerBase
{
    [HttpGet("api/project-info")]
    public async Task<ActionResult<IEnumerable<ProjectInfoModel>>> GetPersonalInfo(string? projectType)
    {
        try
        {
            var result = await projectInfoRepository.GetProjectInfo(projectType);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
