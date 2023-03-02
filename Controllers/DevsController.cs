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
    
    [HttpGet("GetById/{id:length(24)}")]
    public async Task<ActionResult<Dev>> GetById(string id){
        var dev = await _developerService.GetDevAsync(id);

        if (dev == null)
        {
            return NotFound();
        }
        return dev;
    }


    [HttpPost]
    public async Task<IActionResult> Post(DevModel newDevM){
        var newDev = new Dev();

        if (newDevM.F_name.Length >= 3 && newDevM.F_name.Length < 20 )
        {
            newDev.FirstName = newDevM.F_name;
        }else 
        { 
            return BadRequest("The name is too short or too long"); 
        }
        
        if (newDevM.L_name.Length >= 3 && newDevM.L_name.Length < 30 )
        {
            newDev.LastName = newDevM.L_name;
        }else 
        { 
            return BadRequest("The last name is too short or too long"); 
        }
        newDev.FullName = newDevM.F_name + " " + newDevM.L_name;

        if (newDevM.Age > 10)
        {
            newDev.Age = newDevM.Age;
        }else 
        {
            return BadRequest("We only accept peope with age greater than 10");
        }

        if (newDevM.WorkedHours > 30 && newDevM.WorkedHours < 50)
        {
            newDev.WorkedHours = newDevM.WorkedHours;
        }else
        {
            return BadRequest("The worked hours needs to be between 30 and 50 hours");
        }

        if (newDevM.SalaryByHours > 13)
        {
            newDev.SalaryByHours = newDevM.SalaryByHours;
        }else 
        {
            return BadRequest("The salary needs to be greater than 13");
        }

        newDev.DeveloperType = newDevM.Dev_type;
        newDev.email = newDevM.email; 

        await _developerService.CreateAsync(newDev);

        return CreatedAtAction(nameof(Get), new {id = newDev.Id}, newDev);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string id, Dev updateInfo){

        var dev = await _developerService.GetDevAsync(id);

        if(dev is null){
            return NotFound();
        }
        updateInfo.Id = dev.Id;

        await _developerService.UpdateOneAsync(id, updateInfo);
        return NoContent();

    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id){
        var dev = await _developerService.GetDevAsync(id);

        if (dev is null)
        {
            return NoContent();
        }

        await _developerService.RemoveOneAsync(id);

        return NoContent(); 
    }

    public class DevModel{
        public string F_name {get; set;} = null!;
        public string L_name {get;set;} = null!;
        public string Dev_type {get; set;} = null!;
        public int Age {get; set;}
        public decimal SalaryByHours {get; set;}
        public string email {get; set;} = null!;
        public decimal WorkedHours {get; set;}
    }
}