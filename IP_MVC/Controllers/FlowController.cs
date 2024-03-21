using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IP_MVC.Helpers;
using System.Linq;

namespace IP_MVC.Controllers
{
    public class FlowController : Controller
    {
        private readonly IFlowManager _flowManager;

        public FlowController(IFlowManager flowManager)
        {
            _flowManager = flowManager;
        }

        public IActionResult Flow(int id) => View(_flowManager.GetParentFlowsByProjectId(id));

        public IActionResult SubFlow(int id) => View(_flowManager.GetFlowsByParentId(id));
        
        public IActionResult PlayFlow(int id)
        {
            // Retrieve the flow and its associated questions.
            var questionIds = _flowManager.GetQuestionsByFlowId(id).Select(q => q.Id).ToList();

            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues") ?? new Dictionary<int, Queue<int>>();

            // Add the new queue to the dictionary.
            queues[id] = new Queue<int>(questionIds);

            // Store the dictionary in the session.
            HttpContext.Session.Set("queues", queues);

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

            // Get the question ID at the current index.
            var questionId = questionQueue.ElementAt(currentIndex);
            var question = _flowManager.GetQuestionById(questionId);

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
    }
}