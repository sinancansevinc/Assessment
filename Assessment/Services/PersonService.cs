using Assessment.Data;
using Assessment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public class PersonService : IPersonService
    {
        private readonly AssessmentDbContext _context;

        public PersonService(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AddPerson(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public void UpdatePerson(Person person)
        {
            _context.Update(person);
            _context.SaveChanges();
        }
        public async Task<List<Person>> GetPersons()
        {
            var persons = await _context.Persons.ToListAsync();

            if (persons.Any())
            {
                return persons;
            }

            return persons = new List<Person>();

        }

        public async Task<Person> GetPersonById(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.UUID == id);
            
            if (person != null)
            {
                return person;
            }

            return person = new Person();
        }

        public async Task DeletePerson(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.UUID == id);

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

        }
    }
}
