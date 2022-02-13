using Assessment.Controllers;
using Assessment.Dtos;
using Assessment.Models;
using Assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestAssessment
{
    public class ReportsControllerTest
    {
        private readonly Mock<IReportService> _mockRepo;
        private readonly ReportsController _controller;

        private List<ReportLocationDto> reportLocationListDtos;
        private List<Contact> contacts;
        public ReportsControllerTest()
        {
            _mockRepo = new Mock<IReportService>();
            _controller = new ReportsController(_mockRepo.Object);

            reportLocationListDtos = new List<ReportLocationDto>
            {
                new ReportLocationDto
                {
                    Location="İstanbul",
                    PersonCount=2
                },
                new ReportLocationDto
                {
                    Location="Ankara",
                    PersonCount=1
                }
            };

            contacts = new List<Contact>()
            {
                new Contact
                {
                    ContactContent="İzmir",
                    TypeId=3,
                    Id=1,
                    PersonId=1

                },
                new Contact
                {
                    ContactContent="İstanbul",
                    TypeId=3,
                    PersonId=2,
                    Id=2

                }
            };

        }
        
        [Theory]
        [InlineData(1)]
        public async void GetLocationCount_ActionRun_ReturnOkResultWithLocations(int typeId)
        {
            var locationCountList = reportLocationListDtos;
            _mockRepo.Setup(x => x.GetLocationCount(typeId)).ReturnsAsync(locationCountList);

            var result = await _controller.GetLocationCountOrderByDescending(typeId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPersons = Assert.IsType<List<ReportLocationDto>>(okResult.Value);

            Assert.Equal<int>(2, returnPersons.Count);
        }

        [Theory]
        [InlineData(1)]
        public async void GetLocationCount_ActionRun_ReturnNotFound(int typeId)
        {
            List<ReportLocationDto> reportLocationDtos = null;
            _mockRepo.Setup(x => x.GetLocationCount(typeId)).ReturnsAsync(reportLocationDtos);
            var result = await _controller.GetLocationCountOrderByDescending(typeId);
            var okResult = Assert.IsType<NotFoundResult>(result);
        }
        [Theory]
        [InlineData("İstanbul",1)]
        public async void GetPersonCountByLocation_ActionRun_ReturnOkObjectResult(string location,int typeId)
        {
            var personCountList = contacts;
            _mockRepo.Setup(x => x.GetPersonCountByLocation(location,typeId)).ReturnsAsync(personCountList);

            var result = await _controller.GetPersonCountByLocation(location,typeId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            _mockRepo.Verify(x => x.GetPersonCountByLocation(location,typeId), Times.Once);


            var returnPersons = Assert.IsType<int>(okResult.Value);

            Assert.Equal<int>(2, returnPersons);
        }
        [Theory]
        [InlineData("İstanbul",1)]
        public async void GetPersonCountByLocation_ActionRun_NotFoundResult(string location,int typeId)
        {
            List<Contact> contacts = null;
            _mockRepo.Setup(x => x.GetPersonCountByLocation(location,typeId)).ReturnsAsync(contacts);
            var result = await _controller.GetPersonCountByLocation(location,typeId);
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("İstanbul",1)]
        public async void GetPhoneCountByLocation_ActionRun_ReturnOkObjectResult(string location, int typeId)
        {
            var phoneCountList = contacts;
            _mockRepo.Setup(x => x.GetPhoneCountByLocation(location,typeId)).ReturnsAsync(phoneCountList);

            var result = await _controller.GetPhoneCountByLocation(location,typeId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            _mockRepo.Verify(x => x.GetPhoneCountByLocation(location,typeId), Times.Once);


            var returnPersons = Assert.IsType<int>(okResult.Value);

            Assert.Equal<int>(2, returnPersons);
        }
        [Theory]
        [InlineData("İstanbul",1)]
        public async void GetPhoneCountByLocation_ActionRun_NotFoundResult(string location,int typeId)
        {
            List<Contact> contacts = null;
            _mockRepo.Setup(x => x.GetPhoneCountByLocation(location, typeId)).ReturnsAsync(contacts);
            var result = await _controller.GetPhoneCountByLocation(location,typeId);
            var okResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
