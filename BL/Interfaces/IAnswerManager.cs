using System.Collections;
using BL.Domain.Answers;

namespace BL.Interfaces;

public interface IAnswerManager : IManager<Answer>
{
    public IEnumerable<Answer> GetAnswersByProjectId(int projectId);
    IEnumerable GetOpenQuestionAnswersByProjectId(int projectId);
    IEnumerable GetMultipleChoiceQuestionAnswersByProjectId(int projectId);
    IEnumerable GetSingleChoiceQuestionAnswersByProjectId(int projectId);
    IEnumerable GetRangeQuestionAnswersByProjectId(int projectId);
}