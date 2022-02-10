using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public interface IPersonService
    {
        Task AddPerson(Person person);
        void UpdatePerson(Person person);
        Task<List<Person>> GetPersons();
        Task DeletePerson(int id);

    }
}
