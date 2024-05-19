using System.ComponentModel.DataAnnotations;
using BL.Domain;
using BL.Domain.Answers;
using BL.Interfaces;
using DAL.Interfaces;
using ValidationContext = AutoMapper.ValidationContext;

namespace BL.Implementations;

public class ProjectManager(IProjectRepository repository) : Manager<Project>(repository), IProjectManager
{
    public Project GetProjectByProjectId(int projectId)
    {
        return repository.ReadProjectByProjectId(projectId);
    }

    public async Task<IEnumerable<Flow>> GetFlowsByProjectIdAsync(int projectId)
    {
        return await repository.GetFlowsByProjectIdAsync(projectId);
    }

    public ValueTask<Project> FindByIdAsync(int id)
    {
        return repository.FindByIdAsync(id);
    }

    public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
    {
        return repository.GetParentFlowsByProjectId(projectId);
    }

    public void StoreAnswers(int id, List<Answer> answers)
    {
        repository.StoreAnswers(id, answers);
    }

    public void ChangeProject(int projectId, string name, string description)
    {
        Project project = repository.ReadProjectByProjectId(projectId);
        project.Name = name;
        project.Description = description;
        repository.UpdateProject(project);
    }

    public IEnumerable<Project> GetProjectsByAdminId(string adminId)
    {
        return repository.GetProjectsByAdminId(adminId);
    }
}