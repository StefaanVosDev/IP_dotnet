using Microsoft.AspNetCore.Mvc;

//TODO: if the namespace is not being changed when renaming the solution, you can make a new one
namespace WebApplication1.Controllers.api;

/// <summary>
/// The TemplatesController class that inherits from the ControllerBase class.
/// This class is responsible for handling HTTP requests related to templates.
/// </summary>
/// <author>Luca Dandois | TCS</author>
/// <version>1.0.0</version>
//TODO: Change the route to the correct controller name
[Route("api/templates")]
[ApiController]
//TODO: Change the Controller name to the correct name.
public class TemplatesController : ControllerBase
{
    /*
    #region Vars

    //TODO: Change the Generic Template to the correct Domain class
    private readonly IManager<Template> _manager;

    #endregion

    #region Constructors

    //TODO: change the generic Template to match the correct Domain class
    public TemplatesController(IManager<Template> manager)
    {
        _manager = manager;
    }

    #endregion

    #region HttpGet

    [HttpGet]
    public async Task<IActionResult> GetAllTemplates()
    {
        var pizzas = await _manager.GetAllAsync();
        return Ok(pizzas);
    }

    [HttpGet("{id}", Name = "GetTemplate")]
    public async Task<IActionResult> GetTemplate(int id)
    {
        var obj = await _manager.GetAsync(id);
        if (obj is null)
        {
            return NotFound("Template is not found");
        }

        return Ok(obj);
    }

    #endregion

    #region HttpPost

    [HttpPost]
    public async Task<IActionResult> PostTemplate([FromBody] Template template)
    {
        await _manager.AddAsync(template);

        return CreatedAtRoute("GetTemplate", new { template.Id }, null);
    }

    #endregion

    #region HttpPut

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTemplate(int id, [FromBody] Template template)
    {
        var templateToUpdate = await _manager.GetAsync(id);
        if (templateToUpdate is null)
        {
            return NotFound("The template record couldn't be found.");
        }

        await _manager.UpdateAsync(templateToUpdate, template);

        return NoContent();
    }

    #endregion

    #region HttpDelete

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(int id)
    {
        var obj = await _manager.GetAsync(id);
        if (obj is null)
        {
            return NotFound("The template record couldn't be found");
        }

        await _manager.DeleteAsync(obj);

        return NoContent();
    }

    #endregion
    */
}