using BL.Domain;
using BL.Domain.Questions;
using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;

namespace IP_MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionManager _questionManager;

        public QuestionController(IQuestionManager questionManager)
        {
            this._questionManager = questionManager;
        }

        [HttpGet]
        public IActionResult Edit(int parentFlowId)
        {
            var question = _questionManager.GetQuestionById(parentFlowId);

            if (question.Type == QuestionType.Range)
            {
                var rangeQuestion = (RangeQuestion) question;
                ViewBag.Min = rangeQuestion.Min;
                ViewBag.Max = rangeQuestion.Max;
            }
            return View(question);
        }

        public async Task<IActionResult> Delete(int parentFlowId)
        {
            var question = _questionManager.GetQuestionById(parentFlowId);
            
            await _questionManager.DeleteAsync(question);
            return RedirectToAction("Edit", "Flow");
        }


        [HttpPost]
        public IActionResult Reorder(int parentFlowId, int newPosition)
        {
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
            var question = CreateQuestion(type, text, media, flowId);
            await _questionManager.AddAsync(question);
            return RedirectToAction("Edit", new { parentFlowId = question.Id });
        }
        
        [HttpGet]
        public IActionResult GetOptions(int id)
        {
            var options = _questionManager.GetOptionsSingleOrMultipleChoiceQuestion(id);
            return Json(options);
        }
        
        [HttpGet]
        public IActionResult GetRangeValues(int id)
        {
            var values = _questionManager.GetRangeQuestionValues(id);
            return Json(values);
        }
        
        [HttpPost]
        public IActionResult UpdateTitle(int id, string text)
        {
            var question = _questionManager.GetQuestionById(id);
            var newQuestion = question;
            newQuestion.Text = text;
            newQuestion.Position = question.Position;
            newQuestion.Type = question.Type;
            newQuestion.Media = question.Media;
            newQuestion.FlowId = question.FlowId;

            _questionManager.UpdateAsync(question, newQuestion);
            return Ok();
        }
    }
}