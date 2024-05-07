using BL.Domain.Questions;
using BL.Interfaces;
using IP_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers.api
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionManager _questionManager;
        
        public QuestionsController(IQuestionManager questionManager)
        {
            _questionManager = questionManager;
        }
        
        [HttpPost("UpdateMultipleChoiceQuestion")]
        public IActionResult UpdateMultipleChoiceQuestion([FromQuery]int id, [FromQuery]string option)
        {
            var question = _questionManager.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }
            
            _questionManager.AddOptionToQuestion(id, option);
            return Ok();
        }
        
        [HttpPost("UpdateRangeQuestion")]
        public IActionResult UpdateRangeQuestion([FromQuery]int id, [FromQuery]int min, [FromQuery]int max)
        {
            var question = _questionManager.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }

            _questionManager.SetRangeQuestionValues(id, min, max);
            return Ok();
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
        
        //implement the deleteOption
        [HttpPost("DeleteOption")]
        public IActionResult DeleteOption([FromQuery]int id, [FromQuery]string option)
        {
            var question = _questionManager.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }
            
            _questionManager.DeleteOptionFromQuestion(id, option);
            return Ok();
        }
    }
}