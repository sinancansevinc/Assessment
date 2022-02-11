using Assessment.Data;
using Assessment.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Services
{
    public class ReportService : IReportService
    {
        private readonly AssessmentDbContext _context;

        public ReportService(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReportLocationListDto>> GetLocationCount()
        {
            var locationInformations = await _context.Contacts.Where(t => t.TypeId == 3).ToListAsync();

            var list = locationInformations.GroupBy(l => l.ContactContent).Select(c => new ReportLocationListDto
            {

                Location = c.Key,
                PersonCount = c.Select(l => l.EmployeeId).Distinct().Count()

            }).OrderByDescending(p => p.PersonCount).ToList();

            return list;
        }
        public async Task<int> GetPersonCountByLocation(string location)
        {
            var locationList = await _context.Contacts.Where(t => t.TypeId == 3 && t.ContactContent == location).ToListAsync();

            return locationList.Count();
        }
        public async Task<int> GetPhoneCountByLocation(string location)
        {
            var locationInformations = await _context.Contacts.Where(t => t.TypeId == 3 && t.ContactContent==location).ToListAsync();

            var result = (from c in locationInformations
                          join p in _context.Contacts on c.EmployeeId equals p.EmployeeId into ps
                          from p in ps.DefaultIfEmpty()
                          where p.TypeId == 1
                          select p).ToList();
                         

            return result.Count();
        }
    }
}
