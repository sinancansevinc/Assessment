using Assessment.Models;
using Assessment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("GetPersons")]
        public async Task<IActionResult> GetPersons()
        {
            var persons = await _personService.GetPersons();
            return Ok(persons);
        }

        [HttpPost("AddPerson")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            try
            {
               await _personService.AddPerson(person);
                return Ok("Person is addedd");
            }
            catch (Exception ex)
            {
                return BadRequest("Person can not be added");
                
            }
        }
        [HttpPost("DeletePerson")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                await _personService.DeletePerson(id);
                return Ok("Person is deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Person can not be deleted");

            }
        }
        [HttpPost("UpdatePerson")]
        public IActionResult UpdatePerson(Person person)
        {
            try
            {
                _personService.UpdatePerson(person);
                return Ok("Person is updated");
            }
            catch (Exception ex)
            {
                return BadRequest("Person can not be updated due to " + ex.Message);

            }
        }

    }
}
