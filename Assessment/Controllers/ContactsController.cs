using Assessment.Services;
using Assessment.ViewModels;
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
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("AddContactInformation")]
        public async Task<IActionResult> AddContactInformation(ContactCreateDto contactCreateDto)
        {

            await _contactService.AddContactInformation(contactCreateDto);
            return Ok("Contact information added");
        }

        [HttpDelete("DeleteContactInformation")]
        public async Task<IActionResult> DeleteContactInformation(int contactId)
        {

            var contact = await _contactService.GetContactInformationById(contactId);

            if (contact == null)
            {
                return NotFound();
            }

            await _contactService.DeleteContactInformation(contactId);
            return NoContent();

        }
        [HttpPost("GetContactInformationsByPersonId")]
        public async Task<IActionResult> GetContactInformationsByPersonId(int personId)
        {
            var informations = await _contactService.GetContactInformationsByPersonId(personId);
            if (informations != null)
            {
                return Ok(informations);
            }

            return NotFound();
        }
    }
}
