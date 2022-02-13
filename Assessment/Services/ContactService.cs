using Assessment.Data;
using Assessment.Dtos;
using Assessment.Models;
using Assessment.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public class ContactService : IContactService
    {
        private readonly AssessmentDbContext _context;

        public ContactService(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AddContactInformation(ContactCreateDto personContactViewModel)
        {
            Contact contact = new Contact
            {
                PersonId = personContactViewModel.PersonId,
                TypeId = personContactViewModel.ContactTypeId,
                ContactContent = personContactViewModel.ContactContent

            };

            await _context.AddAsync(contact);
            await _context.SaveChangesAsync();
        }
        public async Task<Contact> GetContactInformationById(int id)
        {
            var contactInformation = await _context.Contacts.FirstOrDefaultAsync(p => p.Id == id);

            if (contactInformation != null)
            {
                return contactInformation;
            }

            return contactInformation = new Contact();
        }
        public async Task DeleteContactInformation(int contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            _context.Remove(contact);
            await _context.SaveChangesAsync();

        }

        public async Task<List<ContactDto>> GetContactInformationsByPersonId(int employeeId)
        {
            var contactDetails = await (from c in _context.Contacts
                                        join p in _context.Persons on c.PersonId equals p.UUID
                                        join t in _context.ContactTypes on c.TypeId equals t.Id
                                        where p.UUID == employeeId
                                        select new ContactDto
                                        {
                                            Name = p.Name,
                                            Surname = p.Surname,
                                            Company = p.Company,
                                            Type = t.Type,
                                            Content = c.ContactContent

                                        }).ToListAsync();

            return contactDetails;
        }



    }
}

