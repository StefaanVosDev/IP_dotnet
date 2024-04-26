using System.Collections;
using BL.Domain;
using BL.Domain.Answers;
using BL.Domain.Questions;

namespace DAL.Interfaces;

public interface IFlowRepository : IRepository
{
    public IEnumerable<Flow> GetFlowsByParentId(int flowId);
    public Flow getFlowById(int id);
    public IEnumerable<Question> GetQuestionsByFlowId(int id);
    public IEnumerable<Flow> GetFlowsBetweenPositions(int newPosition, int oldPosition);
}