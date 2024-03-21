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
}