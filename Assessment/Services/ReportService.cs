using Assessment.Data;
using Assessment.Dtos;
using Assessment.Models;
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

        public async Task<List<ReportLocationDto>> GetLocationCount(int typeId)
        {
            var locationInformations = await _context.Contacts.Where(t => t.TypeId == typeId).ToListAsync();

            var list = locationInformations.GroupBy(l => l.ContactContent).Select(c => new ReportLocationDto
            {

                Location = c.Key,
                PersonCount = c.Select(l => l.PersonId).Distinct().Count()

            }).OrderByDescending(p => p.PersonCount).ToList();

            return list;
        }
        public async Task<List<Contact>> GetPersonCountByLocation(string location,int typeId)
        {
            var locationList = await _context.Contacts.Where(t => t.TypeId == typeId && t.ContactContent == location).ToListAsync();

            return locationList;
        }
        public async Task<List<Contact>> GetPhoneCountByLocation(string location,int typeId)
        {
            var locationInformations = await _context.Contacts.Where(t => t.TypeId == typeId && t.ContactContent==location).ToListAsync();

            var result = (from c in locationInformations
                          join p in _context.Contacts on c.PersonId equals p.PersonId into ps
                          from p in ps.DefaultIfEmpty()
                          where p.TypeId == 1
                          select p).ToList();


            return result;
        }
    }
}
