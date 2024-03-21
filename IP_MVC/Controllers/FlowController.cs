using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IP_MVC.Helpers;
using System.Linq;
using BL.Domain.Answers;
using BL.Implementations;
using IP_MVC.Models;

namespace IP_MVC.Controllers
{
    public class FlowController : Controller
    {
        private readonly IFlowManager _flowManager;
        private readonly ISessionManager _sessionManager;
        private readonly IProjectManager _projectManager;
        private readonly IQuestionManager _questionManager;

        public FlowController(IFlowManager flowManager, ISessionManager sessionManager, IProjectManager projectManager,
            IQuestionManager questionManager)
        {
            _flowManager = flowManager;
            _sessionManager = sessionManager;
            _projectManager = projectManager;
            _questionManager = questionManager;
        }

        public IActionResult Flow(int id) => View(_projectManager.GetParentFlowsByProjectId(id));

        public IActionResult SubFlow(int id) => View(_flowManager.GetFlowsByParentId(id));

        public IActionResult PlayFlow(int id)
        {
            // Retrieve the flow and its associated questions.
            var questionIds = _flowManager.GetQuestionsByFlowId(id).Select(q => q.Id).ToList();

            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues") ??
                         new Dictionary<int, Queue<int>>();

            // Add the new queue to the dictionary.
            queues[id] = new Queue<int>(questionIds);

            // Store the dictionary in the session.
            HttpContext.Session.Set("queues", queues);

            // Reset the current index.
            HttpContext.Session.SetInt32("currentIndex", 0);

            // Redirect to the first question.
            return RedirectToAction("Question", new { id });
        }

        public IActionResult Question(int id, AnswerViewModel model)
        {
            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues");

            // If the dictionary is null or doesn't contain a queue for the current flow, redirect to the end of the flow.
            if (queues == null || !queues.ContainsKey(id) || !queues[id].Any())
            {
                return RedirectToAction("EndSubFlow");
            }

            // Retrieve the queue of question IDs for the current flow.
            var questionQueue = queues[id];

            // Retrieve the current index from the session.
            int currentIndex = HttpContext.Session.GetInt32("currentIndex") ?? 0;

            // If the index is out of range, redirect to the end of the flow.
            if (currentIndex < 0 || currentIndex >= questionQueue.Count)
            {
                return RedirectToAction("EndSubFlow");
            }
            
            // Retrieve the current question ID from the queue.
            var questionId = questionQueue.ElementAt(currentIndex);
            
            // Retrieve the question based on the ID.
            var question = _questionManager.GetQuestionById(questionId);

            // Increment the current index and store it in the session.
            currentIndex++;
            HttpContext.Session.SetInt32("currentIndex", currentIndex);

            // Display the question view based on the question type.
            return question.Type switch
            {
                QuestionType.RangeEnum => View("Questions/RangeView", question as RangeQuestion),
                QuestionType.MultipleChoice => View("Questions/MultipleChoice", question as MultipleChoiceQuestion),
                QuestionType.Open => View("Questions/Open", question as OpenQuestion),
                QuestionType.SingleChoice => View("Questions/SingleChoice", question as SingleChoiceQuestion),
                _ => throw new Exception("Unknown question type")
            };
        }

        public IActionResult EndSubFlow()
        {
            ViewData["Message"] = "Thank you for completing the subflow!";
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveAndNext(int flowId, int id, AnswerViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Answer))
            {
                SaveAnswer(model, id);
            }

            return RedirectToAction("Question", new { id = flowId });
        }
        
        private void SaveAnswer(AnswerViewModel model, int questionId)
        {
            var answer = new Answer
            {
                Id = _sessionManager.GetSession(User.Identity.Name).Id,
                QuestionId = questionId,
                AnswerText = model.Answer
            };

            _sessionManager.GetSession(User.Identity.Name).Answers.Add(answer);
            _sessionManager.UpdateSession(_sessionManager.GetSession(User.Identity.Name));
        }
    }
}