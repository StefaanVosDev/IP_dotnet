using BL.Domain;
using BL.Domain.Questions;
using BL.Implementations;
using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;
using IP_MVC.Models;
using WebApplication1.Models;

namespace IP_MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionManager _questionManager;
        private readonly UnitOfWork _unitOfWork;

        public QuestionController(IQuestionManager questionManager, UnitOfWork unitOfWork)
        {
            _questionManager = questionManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Edit(int questionId)
        {
            var question = _questionManager.GetQuestionById(questionId);

            if (question.Type == QuestionType.Range)
            {
                var rangeQuestion = (RangeQuestion) question;
                ViewBag.Min = rangeQuestion.Min;
                ViewBag.Max = rangeQuestion.Max;
            }
            return View(question);
        }

        public async Task<IActionResult> Delete(int questionId)
        {
            _unitOfWork.BeginTransaction();
            var question = _questionManager.GetQuestionById(questionId);
            
            await _questionManager.DeleteAsync(question);
            
            _unitOfWork.Commit();
            return RedirectToAction("Edit", "Flow", new {parentFlowId = question.FlowId});
        }


        [HttpPost]
        public IActionResult Reorder(int parentFlowId, int newPosition)
        {
            _unitOfWork.BeginTransaction();
            var question = _questionManager.GetQuestionById(parentFlowId);
            var oldPosition = question.Position;
            question.Position = newPosition;

            List<Question> affectedQuestions;
            if (newPosition < oldPosition)
            {
                // The question has been moved up, so increment the position of all questions between the old and new position
                affectedQuestions = _questionManager.GetQuestionsBetweenPositions(newPosition, oldPosition - 1).ToList();
                foreach (var affectedQuestion in affectedQuestions)
                {
                    affectedQuestion.Position++;
                }
            }
            else
            {
                // The question has been moved down, so decrement the position of all questions between the old and new position
                affectedQuestions = _questionManager.GetQuestionsBetweenPositions(oldPosition + 1, newPosition).ToList();
                foreach (var affectedQuestion in affectedQuestions)
                {
                    affectedQuestion.Position--;
                }
            }

            // Add the initially moved question to the list of affected questions
            affectedQuestions.Add(question);

            // Update all affected questions at once
            _questionManager.UpdateAllAsync(affectedQuestions);

            _unitOfWork.Commit();
            return RedirectToAction("Edit");
        }
        
        [HttpGet]
        public IActionResult Create(int parentFlowId)
        {
            ViewBag.parentFlowId = parentFlowId;
            return View();
        }

        private Question CreateQuestion(QuestionType type, string text, Media media, int flowId)
        {
            switch (type)
            {
                case QuestionType.MultipleChoice:
                    return new MultipleChoiceQuestion { Text = text, Type = type, Media = media, FlowId = flowId };
                case QuestionType.Open:
                    return new OpenQuestion { Text = text, Type = type, Media = media, FlowId = flowId };
                case QuestionType.Range:
                    return new RangeQuestion { Text = text, Type = type, Media = media, FlowId = flowId };
                case QuestionType.SingleChoice:
                    return new SingleChoiceQuestion { Text = text, Type = type, Media = media, FlowId = flowId };
                default:
                    return new Question { Text = text, Type = type, Media = media, FlowId = flowId };
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(string text, QuestionType type, Media media, int flowId)
        {
            _unitOfWork.BeginTransaction();
            var question = CreateQuestion(type, text, media, flowId);
            await _questionManager.AddAsync(question);
            
            _unitOfWork.Commit();
            return RedirectToAction("Edit", new { questionId = question.Id });
         }

        [HttpGet]
        public IActionResult GetRangeValues(int id)
        {
            var values = _questionManager.GetRangeQuestionValues(id);
            return Json(values);
        }

        [HttpGet]
        public IActionResult StartPreview(int id)
        {
            var question = _questionManager.GetQuestionById(id);
            if (question == null)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = "Question not found"
                };
                return View("Error", errorViewModel);
            }

            var questions = _questionManager.GetQuestionsByFlowId(question.FlowId).ToList();
            //get the position of this question in the list
            var currentIndex = questions.IndexOf(question);
            
            return RedirectToAction("RedirectTroughPreview", "Flow", new {redirectedQuestionId = currentIndex, flowId = question.FlowId});
        }
    }
}
