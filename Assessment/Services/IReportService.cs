using Assessment.Dtos;
using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public interface IReportService
    {
        Task<List<ReportLocationDto>> GetLocationCount(int typeId);
        Task<List<Contact>> GetPersonCountByLocation(string location,int typeId);
        Task<List<Contact>> GetPhoneCountByLocation(string location,int typeId);

    }
}
