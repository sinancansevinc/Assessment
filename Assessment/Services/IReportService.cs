using Assessment.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public interface IReportService
    {
        Task<List<ReportLocationListDto>> GetLocationCount();
        Task<int> GetPersonCountByLocation(string location);
        Task<int> GetPhoneCountByLocation(string location);
    }
}
