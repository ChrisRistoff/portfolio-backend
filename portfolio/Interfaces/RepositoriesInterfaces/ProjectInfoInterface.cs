using portfolio.Models;

namespace portfolio.Interfaces;

public interface IProjectInfo
{
    Task<IEnumerable<ProjectInfoModel>> GetProjectInfo(string? projectType);
    Task<ProjectInfoModel> CreateNewProject(CreateProjectDto projectInfo);
    Task<ProjectInfoModel> GetProjectById(int id);
    Task<ProjectInfoModel> UpdateProject(int id, UpdateProjectDto projectInfo);
    Task DeleteProject(int id);
}
