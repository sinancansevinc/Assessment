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
        public async Task<IActionResult> AddContactInformation(ContactCreateDto personContactViewModel)
        {
            try
            {
                await _contactService.AddContactInformation(personContactViewModel);
                return Ok("Contact information added");
            }
            catch
            {
                return BadRequest("Contact information can not be added");
                
            }
        }
        [HttpPost("DeleteContactInformation")]
        public async Task<IActionResult> DeleteContactInformation(int contactId)
        {
            try
            {
                await _contactService.DeleteContactInformation(contactId);
                return Ok("Contact information is deleted");
            }
            catch
            {
                return BadRequest("Contact information can not be deleted");
            }
        }
        [HttpPost("GetContactInformationsByEmployeeId")]
        public async Task<IActionResult> GetContactInformationsByEmployeeId(int employeeId)
        {
            var informations = await _contactService.GetContactInformationsByEmployeeId(employeeId);
            if (informations.Any())
            {
                return Ok(informations);
            }

            return BadRequest("Informations are not found");
        }
    }
}
