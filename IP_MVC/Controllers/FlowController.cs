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
    public class FlowController(
        IFlowManager flowManager,
        ISessionManager sessionManager,
        IProjectManager projectManager,
        IQuestionManager questionManager)
        : Controller
    {
        public IActionResult Flow(int id) => View(projectManager.GetParentFlowsByProjectId(id));

        public IActionResult SubFlow(int id) => View(flowManager.GetFlowsByParentId(id));

        public async Task<IActionResult> PlayFlow(int id, bool linearFlow)
        {
            var newSession = await sessionManager.CreateNewSession(id);
            HttpContext.Session.SetInt32("sessionId", newSession.Id);

            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues") ?? new Dictionary<int, Queue<int>>();
            queues[id] = flowManager.GetQuestionQueueByFlowId(id);
            HttpContext.Session.Set("queues", queues);

            HttpContext.Session.SetInt32("currentIndex", 0);
            return RedirectToAction("Question", new { id, linearFlow = linearFlow.ToString() });
        }

        public IActionResult Question(int id, bool previous, string linearFlow)
        {
            bool.TryParse(linearFlow, out bool linearFlowBool);

            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues");

            // If the dictionary is null or doesn't contain a queue for the current flow, redirect to the end of the flow.
            if (queues == null || !queues.ContainsKey(id) || !queues[id].Any())
            { 
                return RedirectToAction("EndSubFlow");
               
            }
            
            // Retrieve the current index from the session.
            int currentIndex = HttpContext.Session.GetInt32("currentIndex") ?? 0;

            if (previous)
            {
                currentIndex -= 2;
            }
            // Retrieve the queue of question IDs for the current flow.
            var questionQueue = queues[id];
            
            // If the index is out of range, redirect to the end of the flow.
            if (currentIndex < 0 || currentIndex >= questionQueue.Count)
            {
                if (linearFlowBool)
                {
                    return RedirectToAction("EndSubFlow");
                }
                else
                {
                    currentIndex = 0; 
                }
            }

            // Retrieve the current question ID from the queue.
            var questionId = questionQueue.ElementAt(currentIndex);

            // Retrieve the question based on the ID and type.
            var question = questionManager.GetQuestionByIdAndType(questionId);

            // Increment the current index and store it in the session.
            currentIndex++;
            HttpContext.Session.SetInt32("currentIndex", currentIndex);
            
            // Display the question view based on the question type.
            ViewData["currentIndex"] = currentIndex;
            ViewData["linearFlow"] = linearFlowBool;
            
            return View($"Questions/{question.Type}", question);
        }
        
        public IActionResult EndSubFlow()
        {
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            var session = sessionManager.GetSessionById(sessionId);
            if (session == null)
            {
                //TODO: Handle error
                return View();
            }

            var answers = sessionManager.GetAnswersBySessionId(sessionId).ToList();
            var questions = sessionManager.GetQuestionsBySessionId(sessionId).ToList();

            var parentId = flowManager.GetParentFlowIdBySessionId(sessionId);

            var model = new EndSubFlowViewModel
            {
                Questions = questions,
                Answers = answers,
                FlowId = parentId ?? 0
            };

            ViewData["Message"] = "Thank you for completing the subflow!";
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveAndNext(int flowId, int id, AnswerViewModel model, bool linearFlow)
        {
            if (model.Answer == null || !model.Answer.Any()) return RedirectToAction("Question", new { id = flowId });
            var answerText = string.Join(";", model.Answer);
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0; // Retrieve the session Id from the HttpContext
            sessionManager.SaveAnswer(answerText, model.QuestionId, sessionId, true); // Pass the QuestionId from the model to SaveAnswer
            return RedirectToAction("Question", new { id = flowId, previous=false, linearFlow=linearFlow});
        }
        
        [HttpPost]
        public IActionResult SaveAndPrevious(int flowId, int id, AnswerViewModel model)
        {
            if (model.Answer == null || !model.Answer.Any()) return RedirectToAction("Question", new { id = flowId });
            var answerText = string.Join(";", model.Answer);
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0; // Retrieve the session Id from the HttpContext
            sessionManager.SaveAnswer(answerText, model.QuestionId, sessionId); // Pass the QuestionId from the model to SaveAnswer
            return RedirectToAction("Question", new { id = flowId, previous=true, linearFlow=true});
        }
    }
}