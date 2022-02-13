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

            await _personService.AddPerson(person);
            return Ok("Person is added");

        }

        [HttpDelete("DeletePerson")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _personService.GetPersonById(id);

            if (person == null)
            {
                return NotFound();
            }

            await _personService.DeletePerson(id);
            return NoContent();
        }
        [HttpPut("UpdatePerson")]
        public IActionResult UpdatePerson(int id, Person person)
        {
            if (id != person.UUID)
            {
                return BadRequest();
            }

            _personService.UpdatePerson(person);
            return NoContent();
        }

    }
}
