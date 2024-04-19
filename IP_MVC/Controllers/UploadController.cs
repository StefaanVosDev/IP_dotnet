using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IP_MVC.Controllers;

public class UploadController : Controller
{
    private readonly ICloudManager _cloudManager;

    public UploadController(ICloudManager cloudManager)
    {
        _cloudManager = cloudManager;
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file, string fileName, string folderName)
    {
        _cloudManager.UploadFile(file, fileName, folderName);
        return Ok();
        //TODO: Return to flows page
    }
}