using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("api/project-info")]
    [Authorize]
    public async Task<ActionResult<ProjectInfoModel>> CreateProjectInfo(CreateProjectDto projectInfo)
    {
        try
        {
            var result = await projectInfoRepository.CreateNewProject(projectInfo);
            return Created($"/api/project-info", result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/project-info/{id}")]
    public async Task<ActionResult<ProjectInfoModel>> GetProjectById(int id)
    {
        try
        {
            var result = await projectInfoRepository.GetProjectById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("api/project-info/{id}")]
    public async Task<ActionResult<ProjectInfoModel>> UpdateProjectInfo(int id, UpdateProjectDto projectInfo)
    {
        try
        {
            var result = await projectInfoRepository.UpdateProject(id, projectInfo);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
