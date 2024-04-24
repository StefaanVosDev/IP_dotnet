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

        public IActionResult Question(int id, int redirectedQuestionId, string flowType)
        {
            Enum.TryParse(flowType, out FlowType flowTypeEnum);

            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues");

            // If the dictionary is null or doesn't contain a queue for the current flow, redirect to the end of the flow.
            if (queues == null || !queues.ContainsKey(id) || !queues[id].Any())
            {
                return RedirectToAction("EndSubFlow");
            }
            
            // Retrieve the queue of question IDs for the current flow.
            var questionQueue = queues[id];

            var currentIndex = redirectedQuestionId;
            
            // If the index is out of range, redirect to the end of the flow.
            if (currentIndex < 0 || currentIndex>= questionQueue.Count)
            {
                if (flowTypeEnum == FlowType.LINEAR)
                {
                    return RedirectToAction("EndSubFlow");
                }
                currentIndex = 0;
            }

            // Retrieve the current question ID from the queue.
            var questionId = questionQueue.ElementAt(currentIndex);

            // Retrieve the question based on the ID and type.
            var question = questionManager.GetQuestionByIdAndType(questionId);

            // Get the earlier answer given
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            var earlierAnswer = sessionManager.GetAnswerByQuestionId(sessionId, questionId);

            ViewData["currentIndex"] = currentIndex;
            ViewData["flowType"] = flowTypeEnum;
            ViewData["questionCount"] = questionQueue.Count;
            ViewData["earlierAnswer"] = earlierAnswer;

            // Display the question view based on the question type.
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
        public IActionResult SaveAnswerAndRedirect(int flowId, int id, AnswerViewModel model, FlowType flowType, int redirectedQuestionId)
        {
            // If there is no answer given, redirect to the next question
            if (model.Answer == null || !model.Answer.Any())
            {
                return RedirectToAction("Question", new { id = flowId, redirectedQuestionId, flowType = flowType });
            }
            
            // Join the answer, in case of multiple answers
            var answerText = string.Join(";", model.Answer);
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            
            // If no answers are given yet, save the answer
            var newAnswer = new Answer
            {
                QuestionId = model.QuestionId,
                AnswerText = answerText,
                Session = sessionManager.GetSessionById(sessionId)
            };
            
            // If there is no answer given to this question yet, add the answer to the session
            if (sessionManager.GetAnswerByQuestionId(sessionId, model.QuestionId) == null)
            {
                sessionManager.AddAnswerToSession(sessionId, newAnswer, flowType);
            }
            else
            {
                // If an answer is already given, search the old answer and update it
                var oldAnswer = sessionManager.GetAnswerByQuestionId(sessionId, model.QuestionId);
                sessionManager.UpdateAnswer(oldAnswer, newAnswer);
            }

            return RedirectToAction("Question",
                new { id = flowId, redirectedQuestionId = redirectedQuestionId, flowType = flowType });
        }

        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateParentFlow(Flow flow, int? parentFlowId)
        {
            if (!ModelState.IsValid) return View(flow);

            // Set the ProjectId and ParentFlowId of the new flow
            //flow.ProjectId = projectManager.;
            flow.ParentFlowId = parentFlowId;
            
            flowManager.AddAsync(flow);
            return RedirectToAction("Edit", new { id = flow.Id });
        }
        
        [HttpPost]
        public IActionResult CreateSubFlow(Flow flow, int? parentFlowId)
        {
            if (!ModelState.IsValid) return View(flow);

            

            flowManager.AddAsync(flow);
            return RedirectToAction("Edit", new { id = flow.Id });
        }
    }
}