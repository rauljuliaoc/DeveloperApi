using DeveloperApi.Models;
using DeveloperApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class DevsController : ControllerBase
{

    private readonly DeveloperService _developerService;
    

    public DevsController(DeveloperService developerService)=>
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

    [HttpGet("GetByEmail/{email}")]
    public async Task<ActionResult<Dev>> GetByEmail(string email){
        var dev = await _developerService.GetDevByEmailAsync(email);

        if (dev is null)
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

        if(SetDevValueType(newDevM.Dev_type) is null)
        {
            return BadRequest("The developer type needs to be: junior, senior, intermediate or lead");
        }else
        {
            newDev.DeveloperType = SetDevValueType(newDevM.Dev_type);
        }

        if(newDevM.email is null)
        {
            return BadRequest("The email can't be null or empty");
        }else
        {   
            newDev.email = newDevM.email; 
        }

        await _developerService.CreateAsync(newDev);

        return CreatedAtAction(nameof(Get), new {id = newDev.Id}, newDev);
    }

    [HttpPut("UpdateDev/{email}")]
    public async Task<IActionResult> UpdateDev(string email, DevModel updateInfo){

        var dev = await _developerService.GetDevByEmailAsync(email);

        if(dev is null){
            return NotFound();
        }

        dev.FirstName = (updateInfo.F_name is null)? dev.FirstName : updateInfo.F_name;
        dev.LastName = (updateInfo.L_name is null)? dev.LastName : updateInfo.L_name;

        if (updateInfo.Age =! null)
        {
            if (updateInfo > 10 )
            {
                dev.Age = updateInfo.Age;

            }else
            {
                return BadRequest("We only accept peope with age greater than 10");
            } 
        }

        dev.email = (updateInfo.email is null)? dev.email : updateInfo.email;

        //await _developerService.UpdateOneAsync(id, updateInfo); 
        return NoContent();

    }

    [HttpDelete("Delete/{email}")]
    public async Task<IActionResult> Delete(string email){
        var dev = await _developerService.GetDevByEmailAsync(email);

        if (dev is null)
        {
            return NoContent();
        }

        await _developerService.RemoveOneAsync(email);

        return NoContent(); 
    }

    private static string SetDevValueType(string type){
        if(type == "junior")
        {
            return DeveloperType.Junior.ToString();
        }else if (type == "intermediate")
        {
            return DeveloperType.Intermediate.ToString();
        } else if(type == "senior")
        {
            return DeveloperType.Senior.ToString();
        }else if(type == "lead")
        {
            return DeveloperType.Lead.ToString();
        }else {

            return null;
        }
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