using BL.Domain;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IP_MVC.Helpers;
using BL.Domain.Answers;
using BL.Implementations;
using IP_MVC.Models;
using WebApplication1.Models;

namespace IP_MVC.Controllers
{
    public class FlowController(
        IFlowManager flowManager,
        ISessionManager sessionManager,
        IProjectManager projectManager,
        IQuestionManager questionManager,
        UnitOfWork unitOfWork)
        : Controller
    {
        public async Task<ViewResult> Flow(int projectId, int? parentFlowId)
        {
            var active = HttpContext.Session.Get<bool>("projectActive");
            ViewBag.ProjectId = projectId;
            ViewBag.ParentFlowId = parentFlowId;
            ViewBag.ActiveProject = active;
            var flows = await projectManager.GetParentFlowsByProjectIdAsync(projectId);
            if (active)
            {
                flows = await projectManager.GetAvailableFlowsByProjectIdAsync(projectId);
            }

            var isParentFlow = new Dictionary<int, bool>();
            var containsQuestions = new Dictionary<int, bool>();

            foreach (var flow in flows)
            {
                isParentFlow[flow.Id] = flowManager.IsParentFlow(flow.Id);
                containsQuestions[flow.Id] = questionManager.GetQuestionsByFlowId(flow.Id).Any();
            }

            ViewBag.IsParentFlow = isParentFlow;
            ViewBag.ContainsQuestions = containsQuestions;
            return View(flows);
        }

        public IActionResult SubFlow(int parentFlowId, bool active = false)
        {
            ViewBag.ProjectId = flowManager.GetFlowById(parentFlowId).ProjectId;
            ViewBag.ParentFlowId = parentFlowId;
            ViewBag.ActiveProject = HttpContext.Session.Get<bool>("projectActive");
            ViewBag.ActiveProject = active;

            return View(flowManager.GetFlowsByParentId(parentFlowId));
        }

        public IActionResult ActivateProject(int projectId, bool active)
        {
            HttpContext.Session.Set("projectActive", active);
            if (active)
            {
                return RedirectToAction("Flow", new { projectId });
            }

            return RedirectToAction("Project", "Project");
        }

        public async Task<IActionResult> PlayFlow(int parentFlowId, FlowType flowType, bool active)
        {
            unitOfWork.BeginTransaction();

            ViewBag.ActiveProject = HttpContext.Session.Get<bool>("projectActive");
            ViewBag.ActiveProject = active;

            var newSession = await sessionManager.CreateNewSession(parentFlowId);
            unitOfWork.Commit();

            HttpContext.Session.SetInt32("sessionId", newSession.Id);

            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues") ??
                         new Dictionary<int, Queue<int>>();
            queues[parentFlowId] = await flowManager.GetQuestionQueueByFlowIdAsync(parentFlowId);
            HttpContext.Session.Set("queues", queues);

            HttpContext.Session.SetInt32("currentIndex", 0);

            HttpContext.Session.Set("flowType", flowType);
            HttpContext.Session.SetInt32("parentFlowId", parentFlowId);

            return RedirectToAction("Question", new { id = newSession.Id });
        }

        public IActionResult Question(int id, int redirectedQuestionId)
        {
            ViewBag.ActiveProject = HttpContext.Session.Get<bool>("projectActive");

            // Retrieve the dictionary of queues from the session.
            var queues = HttpContext.Session.Get<Dictionary<int, Queue<int>>>("queues");

            // Retrieve the flow type from the session.
            var flowType = HttpContext.Session.Get<FlowType>("flowType");

            // Retrieve parentFlowId from the session.
            var parentFlowId = HttpContext.Session.GetInt32("parentFlowId") ?? 0;

            // If the dictionary is null or doesn't contain a queue for the current flow, redirect to the end of the flow.
            if (queues == null || !queues.ContainsKey(parentFlowId) || !queues[parentFlowId].Any())
            {
                return RedirectToAction("EndSubFlow");
            }

            // Retrieve the queue of question IDs for the current flow.
            var questionQueue = queues[parentFlowId];

            var currentIndex = redirectedQuestionId;

            // If the index is out of range, redirect to the end of the flow.
            if (currentIndex < 0 || currentIndex >= questionQueue.Count)
            {
                if (flowType == FlowType.LINEAR)
                {
                    return RedirectToAction("EndSubFlow");
                }

                currentIndex = 0;
            }

            // Retrieve the current question ID from the queue.
            var questionId = questionQueue.ElementAt(currentIndex);

            // Haal de huidige vraag op
            var question = questionManager.GetQuestionByIdAndType(questionId);

            // Maak een QuestionViewModel aan en vul het met de vraag en het type
            var viewModel = new QuestionViewModel
            {
                Question = question,
                QuestionType = question.Type
            };

            // Get the earlier answer given
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            var earlierAnswer = sessionManager.GetAnswerByQuestionId(sessionId, questionId);

            ViewData["currentIndex"] = currentIndex;
            ViewData["questionCount"] = questionQueue.Count;
            ViewData["earlierAnswer"] = earlierAnswer;
            ViewBag.FlowType = flowType;
            ViewBag.subFlowId = flowManager.GetParentFlowIdBySessionId(id);
            ViewBag.QuestionId = questionId;
            ViewBag.Preview = false;

            return View("Questions/Questions", viewModel);
        }

        public IActionResult EndSubFlow()
        {
            ViewBag.ActiveProject = HttpContext.Session.Get<bool>("projectActive");

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
        public IActionResult SaveAnswerAndRedirect(int flowId, int id, AnswerViewModel model, int redirectedQuestionId)
        {
            // If there is no answer given, redirect to the next question
            if (model.Answer == null || !model.Answer.Any())
            {
                return RedirectToAction("Question", new { id = flowId, redirectedQuestionId });
            }

            // Join the answer, in case of multiple answers
            var answerText = string.Join("\n", model.Answer);
            var sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
            var flowType = HttpContext.Session.Get<FlowType>("flowType");
            var flow = flowManager.GetFlowById(flowId);

            // If no answers are given yet, save the answer
            var newAnswer = new Answer
            {
                QuestionId = model.QuestionId,
                AnswerText = answerText,
                Session = sessionManager.GetSessionById(sessionId),
                Flow = flow
            };
            unitOfWork.BeginTransaction();

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

            unitOfWork.Commit();
            return RedirectToAction("Question",
                new { id = flowId, redirectedQuestionId });
        }

        
        public IActionResult Delete(int flowId)
        {
            unitOfWork.BeginTransaction();
            var flowToRemove = flowManager.GetFlowById(flowId);
            var projectId1 = flowToRemove.ProjectId;

            var subFlows = flowManager.GetFlowsByParentId(flowId);
            foreach (var subFlow in subFlows)
            {
                flowManager.DeleteAsync(subFlow);
            }

            flowManager.DeleteAsync(flowToRemove);
            unitOfWork.Commit();
            
            return RedirectToAction("Flow", new { projectId = projectId1 });
        }

        [HttpGet]
        public IActionResult Edit(int parentFlowId)
        {
            ViewBag.ActiveProject = false;
            ViewBag.FlowId = parentFlowId;
            
            var flow = flowManager.GetFlowById(parentFlowId);
            var questions = questionManager.GetQuestionsByFlowId(parentFlowId).OrderBy(q => q.Position);

            var model = new FlowEditViewModel
            {
                Flow = flow,
                Questions = questions
            };
            var active = HttpContext.Session.Get<bool>("projectActive");
            ViewBag.ActiveProject = active;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int parentFlowId, FlowEditViewModel newFlowModel)
        {
            unitOfWork.BeginTransaction();
            var existingFlow = flowManager.GetFlowById(parentFlowId);

            if (existingFlow == null)
            {
                unitOfWork.Commit();
                return NotFound();
            }

            var newFlow = existingFlow;
            newFlow.Name = newFlowModel.Flow.Name;
            newFlow.Description = newFlowModel.Flow.Description;
            newFlow.StartDate = newFlowModel.Flow.StartDate.ToUniversalTime();
            newFlow.EndDate = newFlowModel.Flow.EndDate.ToUniversalTime();

            flowManager.UpdateAsync(existingFlow, newFlow);

            unitOfWork.Commit();
            return RedirectToAction("Flow", new { projectId = newFlow.ProjectId });
        }
        
        
        public IActionResult Create(Flow flow, int projectId, int? parentFlowId)
        {
            unitOfWork.BeginTransaction();
            //if (!ModelState.IsValid) return View(flow);

            flow.ProjectId = projectId;
            flow.ParentFlowId = parentFlowId;
            flow.StartDate = flow.StartDate.ToUniversalTime();
            flow.EndDate = flow.EndDate.ToUniversalTime();
            flowManager.AddAsync(flow);

            unitOfWork.Commit();

            if (parentFlowId == null)
            {
                return RedirectToAction("Flow", new { ProjectId = projectId });
            }

            return RedirectToAction("Flow", new { parentFlowId = flow.ParentFlowId });
        }

        [HttpPost]
        public IActionResult Reorder(int parentFlowId, int newPosition)
        {
            unitOfWork.BeginTransaction();
            var flow = flowManager.GetFlowById(parentFlowId);
            var oldPosition = flow.Position;
            flow.Position = newPosition;

            List<Flow> affectedFlows;
            if (newPosition < oldPosition)
            {
                // The flow has been moved up, so increment the position of all flows between the old and new position
                affectedFlows = flowManager.GetFlowsBetweenPositions(newPosition, oldPosition - 1).ToList();
                foreach (var affectedFlow in affectedFlows)
                {
                    affectedFlow.Position++;
                }
            }
            else
            {
                // The flow has been moved down, so decrement the position of all flows between the old and new position
                affectedFlows = flowManager.GetFlowsBetweenPositions(oldPosition + 1, newPosition).ToList();
                foreach (var affectedFlow in affectedFlows)
                {
                    affectedFlow.Position--;
                }
            }

            // Add the initially moved flow to the list of affected flows
            affectedFlows.Add(flow);

            // Update all affected flows at once
            flowManager.UpdateAllAsync(affectedFlows);

            unitOfWork.Commit();
            return RedirectToAction("Flow");
        }

        public IActionResult RedirectTroughPreview(int redirectedQuestionId, int flowId)
        {
            var questionsByFlowId = questionManager.GetQuestionsByFlowIdWithMedia(flowId).ToList();

            if (redirectedQuestionId < 0 || redirectedQuestionId >= questionsByFlowId.Count)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = "Question not found"
                };
                return View("Error", errorViewModel);
            }

            var question = questionsByFlowId[redirectedQuestionId];

            var viewModel = new QuestionViewModel()
            {
                Question = question,
                QuestionType = question.Type
            };
            ViewData["currentIndex"] = redirectedQuestionId;
            ViewData["questionCount"] = questionManager.GetQuestionsByFlowId(question.FlowId).Count();
            ViewBag.FlowType = question.Type;
            ViewBag.Preview = true;

            return View($"~/Views/Flow/Questions/Questions.cshtml", viewModel);
        }
    }
}