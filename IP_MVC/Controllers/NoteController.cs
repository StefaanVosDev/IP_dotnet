using BL.Domain;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class NoteController(
    IQuestionManager questionManager,
    ISessionManager sessionManager,
    INoteManager noteManager) : Controller
{
    [HttpGet]
    public IActionResult AddNote()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateNote(int questionId, string content)
    {
        int sessionId = HttpContext.Session.GetInt32("sessionId") ?? 0;
        // Convert the questionId and sessionId from string to int
        if (questionId == 0 || sessionId == 0)
        {
            return BadRequest("Invalid questionId or sessionId");
        }

        // Check if the questionId and sessionId exist in the database
        var question = questionManager.GetQuestionById(questionId);
        var session = sessionManager.GetSessionById(sessionId);
        if (question == null || session == null)
        {
            return NotFound("Question or Session not found");
        }

        // Create a new Note object with the provided note string
        var newNote = new Note { Content = content, QuestionId = questionId, SessionId = sessionId };
        
        //Save the changes to the database
        noteManager.AddAsync(newNote);
        
        // return Ok(newNote);
        return NoContent();
    }
}