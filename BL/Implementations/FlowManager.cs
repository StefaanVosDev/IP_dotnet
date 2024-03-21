using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.Implementations
{
    public class FlowManager(IFlowRepository repository) : Manager<Flow>(repository), IFlowManager
    {

        public IEnumerable<Flow> GetFlowsByProjectId(int projectId)
        {
            return repository.GetFlowsByProjectIdAsync(projectId);
        }

        public IEnumerable<Flow> GetParentFlowsByProjectId(int projectId)
        {
            return repository.GetParentFlowsByProjectId(projectId);
        }

        public IEnumerable<Flow> GetFlowsByParentId(int flowId)
        {
            return repository.GetFlowsByParentId(flowId);
        }

        public Flow GetFlowById(int id)
        {
            return repository.getFlowById(id);
        }

        public Flow GetFirstSubFlowByParentId(int id)
        {
            return repository.GetFlowsByParentId(id).FirstOrDefault();
        }

        public IEnumerable<Question> GetQuestionsByFlowId(int id)
        {
            return repository.GetQuestionsByFlowId(id);
        }

        public Question GetQuestionById(int questionId)
        {
            return repository.GetQuestionById(questionId);
        }
    }
}