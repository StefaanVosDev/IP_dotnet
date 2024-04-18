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

        public async Task<IActionResult> PlayFlow(int id, FlowType flowType)
        {
            var newSession = await sessionManager.CreateNewSession(id);
            HttpContext.Session.SetInt32("sessionId", newSession.Id);

            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues") ??
                         new Dictionary<int, Queue<int>>();
            queues[id] = flowManager.GetQuestionQueueByFlowId(id);
            HttpContext.Session.Set("queues", queues);

            HttpContext.Session.SetInt32("currentIndex", 0);
            return RedirectToAction("Question", new { id, flowType = flowType.ToString() });
        }

        public IActionResult Question(int id, bool previous, string flowType)
        {
            Enum.TryParse(flowType, out FlowType flowTypeEnum);

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
                if (flowTypeEnum == FlowType.LINEAR)
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
            ViewData["flowType"] = flowTypeEnum;

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
        public IActionResult SaveAndNext(int flowId, int id, AnswerViewModel model, FlowType flowType)
        {
            return SaveAnswerAndRedirect(flowId, id, model, flowType, false);
        }

        [HttpPost]
        public IActionResult SaveAndPrevious(int flowId, int id, AnswerViewModel model)
        {
            return SaveAnswerAndRedirect(flowId, id, model, FlowType.LINEAR, true);
        }

        private IActionResult SaveAnswerAndRedirect(int flowId, int id, AnswerViewModel model, FlowType flowType, bool previous)
        {
            if (model.Answer == null || !model.Answer.Any())
            {
                return RedirectToAction("Question", new { id = flowId, previous = previous, flowType = flowType });
            }

            var answerText = string.Join(";", model.Answer);
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            sessionManager.SaveAnswer(answerText, model.QuestionId, sessionId, flowType);

            return RedirectToAction("Question", new { id = flowId, previous = previous, flowType = flowType });
        }
    }
}