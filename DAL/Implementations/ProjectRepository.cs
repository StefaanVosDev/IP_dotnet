using BL.Domain;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class ProjectRepository(PhygitalDbContext context) : Repository(context), IProjectRepository
{
    private readonly DbContext _context = context;

    
}