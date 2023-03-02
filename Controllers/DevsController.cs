using DeveloperApi.Models;
using DeveloperApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class DevsController : ControllerBase
{

    private readonly DeveloperService _developerService;

    public DevsController(DeveloperService developerService) =>
        _developerService = developerService;

    [HttpGet]
    public async Task<List<Dev>> Get()=>
        await _developerService.GetDevsAsync();
    
    [HttpGet("{id : lenght(24)}")]
    public async Task<ActionResult<Dev>> GetById(string id){
        var dev = await _developerService.GetDevAsync(id);

        if (dev == null)
        {
            return NotFound();
        }
        return dev;
    }


    [HttpPost]
    public async Task<IActionResult> Post(Dev newDev){
        await _developerService.CreateAsync(newDev);

        return CreatedAtAction(nameof(Get), new {id = newDev.Id}, newDev);
    }

    [HttpPut("id : lenght(24)")]
    public async Task<IActionResult> Put(string id, Dev updateInfo){

        var dev = await _developerService.GetDevAsync(id);

        if(dev is null){
            return NotFound();
        }
        updateInfo.Id = dev.Id;

        await _developerService.UpdateOneAsync(id, updateInfo);
        return NoContent();

    }

    [HttpDelete("id: lenght(24)")]
    public async Task<IActionResult> Delete(string id){
        var dev = await _developerService.GetDevAsync(id);

        if (dev is null)
        {
            return NoContent();
        }

        await _developerService.RemoveOneAsync(id);

        return NoContent(); 
    }
}