using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class NoteRepository(PhygitalDbContext context) : Repository(context), INoteRepository
{
    
}