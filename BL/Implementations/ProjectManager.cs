using BL.Domain;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations;

public class ProjectManager(IProjectRepository repository) : Manager<Project>(repository), IProjectManager
{
    
}