using BL.Domain;
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
        return _context.Set<Question>().Where(q => q.FlowId == flowId).Include(q => q.Media).ToList();
    }
    
    public IEnumerable<Question> GetQuestionsByFlowIdWithMedia(int id)
    {
        return _context.Set<Question>().Where(q => q.FlowId == id).Include(q => q.Media).ToList();
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
        if (singleChoiceQuestion != null)
        {
            singleChoiceQuestion.Options ??= new List<string>();
            singleChoiceQuestion.Options.Add(option);
        }
        else
        {
            var multipleChoiceQuestion = _context.Set<MultipleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
            if (multipleChoiceQuestion != null)
            {
                multipleChoiceQuestion.Options ??= new List<string>();

                multipleChoiceQuestion.Options.Add(option);
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
        }
    }

    public void DeleteOptionFromQuestion(int id, string option)
    {
        var singleChoiceQuestion = _context.Set<SingleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
        if (singleChoiceQuestion != null)
        {
            singleChoiceQuestion.Options.Remove(option);
        }
        else
        {
            var multipleChoiceQuestion = _context.Set<MultipleChoiceQuestion>().FirstOrDefault(q => q.Id == id);
            if (multipleChoiceQuestion != null)
            {
                multipleChoiceQuestion.Options.Remove(option);
            }
        }
    }

    public void AddMediaToQuestion(int questionId, string path, string description, MediaType type)
    {
        var media = new Media()
        {
            url = path,
            description = description,
            type = type
        };
        _context.Set<Question>().FirstOrDefault(q => q.Id == questionId)!.Media = media;
    }
}