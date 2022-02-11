using Assessment.Dtos;
using Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public interface IContactService
    {
        Task AddContactInformation(ContactCreateDto personContactViewModel);
        Task DeleteContactInformation(int contactId);
        Task<List<ContactListDto>> GetContactInformationsByEmployeeId(int employeeId);
    }
}
