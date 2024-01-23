using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class ProjectInfoController(ProjectInfoRepository projectInfoRepository) : ControllerBase
{
    [HttpGet("apis/project-info")]
    public async Task<ActionResult<IEnumerable<ProjectInfoModel>>> GetProjectsInfo(string? projectType)
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

    [HttpPost("apis/project-info")]
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

    [HttpGet("apis/project-info/{id}")]
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

    [HttpPatch("apis/project-info/{id}")]
    [Authorize]
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

    [HttpDelete("apis/project-info/{id}")]
    [Authorize]
    public async Task<ActionResult<ProjectInfoModel>> DeleteProjectInfo(int id)
    {
        try
        {
            var project = await projectInfoRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            await projectInfoRepository.DeleteProject(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
