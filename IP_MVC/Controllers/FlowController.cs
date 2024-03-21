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

            // Create a new session.
            var newSession = new Session
            {
                FlowId = id,
                Answers = new List<Answer>()
            };

            // Save the new session.
            _sessionManager.AddAsync(newSession).Wait();
            
            // Store the session Id in the HttpContext.
            HttpContext.Session.SetInt32("sessionId", newSession.Id);

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

        public IActionResult Question(int id)
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
            if (model.Answer != null && model.Answer.Any())
            {
                var answerText = string.Join(",", model.Answer);
                var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0; // Retrieve the session Id from the HttpContext
                SaveAnswer(answerText, id, sessionId); // Pass the session Id to SaveAnswer
            }

            return RedirectToAction("Question", new { id = flowId });
        }

        private void SaveAnswer(string answerText, int questionId, int sessionId)
        {
            var session = _sessionManager.GetSessionById(sessionId);
            if (session == null)
            {
                //TODO: Handle error
                return;
            }

            var answer = new Answer
            {   Session = session,
                QuestionId = questionId,
                AnswerText = answerText
            };
            
            _sessionManager.AddAnswerToSession(sessionId, answer);
        }
    }
}