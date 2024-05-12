using BL.Domain;
using BL.Domain.Questions;
using BL.Interfaces;
using Google.Cloud.Storage.V1;
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
        
        [HttpPost("UploadMedia")]
        public async Task<IActionResult> UploadMedia([FromForm]IFormFile file, [FromForm]int questionId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }

                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Upload the file to Google Cloud Storage
                var storage = StorageClient.Create();
                using var fileStream = System.IO.File.OpenRead(filePath);
                var objectName = $"Questions/{file.FileName}";
                storage.UploadObject("phygital-public", objectName, null, fileStream);
                Console.WriteLine($"Uploaded {objectName}.");

                // Save the URL of the uploaded file in your database
                var fileUrl = $"https://storage.googleapis.com/phygital-public/{objectName}";
                
                var description = "Uploaded media";
                // get the media type from the file extension
                var mediaType = file.ContentType switch
                {
                    "video/mp4" => MediaType.VIDEO,
                    "image/jpeg" => MediaType.IMAGE,
                    "image/png" => MediaType.IMAGE,
                    "audio/mpeg" => MediaType.AUDIO,
                    _ => throw new Exception("Unsupported media type")
                };
                _questionManager.AddMediaToQuestion(questionId, fileUrl, description, mediaType);

                return Ok(new { filePath = fileUrl });
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                // Return a 500 status code with a detailed error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}