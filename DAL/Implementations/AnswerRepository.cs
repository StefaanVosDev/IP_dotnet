// AnswerRepository.cs

using BL.Domain;
using BL.Domain.Answers;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class AnswerRepository(PhygitalDbContext context) : Repository(context), IAnswerRepository
    {
        private readonly PhygitalDbContext _context = context;

        public IEnumerable<Answer> GetAnswersByProjectId(int projectId)
        {
            return _context.Set<Answer>()
                .Include<Answer, Flow>(a => a.Flow)
                .Where(a => a.Flow != null && a.Flow.ProjectId == projectId)
                .ToList<Answer>();
        }
    }
}