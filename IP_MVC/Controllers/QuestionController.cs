using BL.Domain.Questions;
using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;

namespace IP_MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionManager questionManager;

        public QuestionController(IQuestionManager questionManager)
        {
            this.questionManager = questionManager;
        }

        [HttpGet]
        public IActionResult Edit(int parentFlowId)
        {
            var question = questionManager.GetQuestionById(parentFlowId);
            return View(question);
        }

        [HttpPost]
        public IActionResult Delete(int parentFlowId)
        {
            var question = questionManager.GetQuestionById(parentFlowId);
            questionManager.DeleteAsync(question);
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public IActionResult Reorder(int parentFlowId, int newPosition)
        {
            var question = questionManager.GetQuestionById(parentFlowId);
            var oldPosition = question.Position;
            question.Position = newPosition;

            List<Question> affectedQuestions;
            if (newPosition < oldPosition)
            {
                // The question has been moved up, so increment the position of all questions between the old and new position
                affectedQuestions = questionManager.GetQuestionsBetweenPositions(newPosition, oldPosition - 1).ToList();
                foreach (var affectedQuestion in affectedQuestions)
                {
                    affectedQuestion.Position++;
                }
            }
            else
            {
                // The question has been moved down, so decrement the position of all questions between the old and new position
                affectedQuestions = questionManager.GetQuestionsBetweenPositions(oldPosition + 1, newPosition).ToList();
                foreach (var affectedQuestion in affectedQuestions)
                {
                    affectedQuestion.Position--;
                }
            }

            // Add the initially moved question to the list of affected questions
            affectedQuestions.Add(question);

            // Update all affected questions at once
            questionManager.UpdateAllAsync(affectedQuestions);

            return RedirectToAction("Edit");
        }
        
        [HttpGet]
        public IActionResult Create(int parentflowid)
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (!ModelState.IsValid) return View(question);
            questionManager.AddAsync(question);
            return RedirectToAction("Edit", new { id = question.Id });

        }
    }
}