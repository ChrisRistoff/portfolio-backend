using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class ProjectInfoController(ProjectInfoRepository projectInfoRepository) : ControllerBase
{
    [HttpGet("api/project-info")]
    public async Task<OkObjectResult> GetPersonalInfo(string? projectType)
    {
        var result = await projectInfoRepository.GetProjectInfo(projectType);
        return Ok(result);
    }
}
