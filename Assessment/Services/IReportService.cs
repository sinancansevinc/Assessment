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
        Task<List<ReportLocationDto>> GetLocationCount();
        Task<List<Contact>> GetPersonCountByLocation(string location);
        Task<List<Contact>> GetPhoneCountByLocation(string location);

    }
}
