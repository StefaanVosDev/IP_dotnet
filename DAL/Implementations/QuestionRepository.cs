using BL.Domain.Questions;
using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class QuestionRepository(PhygitalDbContext context) : Repository(context), IQuestionRepository
{
    private readonly DbContext _context = context;

    public Question GetQuestionByIdWithMedia(int questionId)
    {
        return
            _context.Set<Question>().Include(q => q.Media).FirstOrDefault(q => q.Id == questionId);
    }

    public IEnumerable<Question> GetQuestionsByFlowId(int flowId)
    {
        return _context.Set<Question>().Where(q => q.FlowId == flowId).ToList();
    }

    public IEnumerable<Question> GetQuestionsBetweenPositions(int newPosition, int oldPosition)
    {
        return _context.Set<Question>().Where(q => q.Position >= newPosition && q.Position <= oldPosition).ToList();
    }

    public List<string> GetOptionsSingleOrMultipleChoiceQuestion(int id)
    {
        //get the options from a single choice or multiple choice question
        var singleChoiceOptions = _context.Set<SingleChoiceQuestion>().FirstOrDefault(q => q.Id == id)?.Options; 
        if (singleChoiceOptions != null)
            return singleChoiceOptions;
        
        return _context.Set<MultipleChoiceQuestion>().FirstOrDefault(q => q.Id == id)?.Options;
    }

    public (int, int) GetRangeQuestionValues(int id)
    {
        var rangeQuestion = _context.Set<RangeQuestion>().FirstOrDefault(q => q.Id == id);
        if (rangeQuestion != null) 
            return (rangeQuestion.Min, rangeQuestion.Max);
        
        return (0, 0);
    }

    public void AddOptionToQuestion(int id, string option)
    {
        var singleChoiceQuestion = _context.Set<SingleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
        if (singleChoiceQuestion != null) {
            if (singleChoiceQuestion.Options == null)
                singleChoiceQuestion.Options = new List<string>();
            singleChoiceQuestion.Options.Add(option);
            _context.SaveChanges();
        }
        else
        {
            var multipleChoiceQuestion = _context.Set<MultipleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
            if (multipleChoiceQuestion != null)
            {
                //if the list is null, first make a new list
                if (multipleChoiceQuestion.Options == null)
                    multipleChoiceQuestion.Options = new List<string>();
                
                multipleChoiceQuestion.Options.Add(option);
                _context.SaveChanges();
            }
        }
    }

    public void SetRangeQuestionValues(int id, int min, int max)
    {
        var rangeQuestion = _context.Set<RangeQuestion>().FirstOrDefault(q => q.Id == id);
        if (rangeQuestion != null)
        {
            rangeQuestion.Min = min;
            rangeQuestion.Max = max;
            _context.SaveChanges();
        }
    }

    public void DeleteOptionFromQuestion(int id, string option)
    {
        var singleChoiceQuestion = _context.Set<SingleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
        if (singleChoiceQuestion != null)
        {
            singleChoiceQuestion.Options.Remove(option);
            _context.SaveChanges();
        }
        else
        {
            var multipleChoiceQuestion = _context.Set<MultipleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
            if (multipleChoiceQuestion != null)
            {
                multipleChoiceQuestion.Options.Remove(option);
                _context.SaveChanges();
            }
        }
    }
}