using BL.Domain.Questions;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class QuestionRepository(PhygitalDbContext context) : Repository(context), IQuestionRepository
{
    private readonly DbContext _context = context;

    public Question GetQuestionById(int questionId)
    {
        return _context.Set<Question>().Find(questionId);
    }

    public IEnumerable<Question> GetQuestionsByFlowId(int flowId)
    {
        return _context.Set<Question>().Where(q => q.FlowId == flowId).ToList();
    }

    public IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition)
    {
        return _context.Set<Question>().Where(q => q.Position >= newPosition && q.Position <= oldPosition).ToList();
    }
}