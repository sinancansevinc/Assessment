using Assessment.Controllers;
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
    public class PersonControllerTest
    {
        private readonly Mock<IPersonService> _mockRepo;
        private readonly PersonsController _controller;

        private List<Person> persons;

        public PersonControllerTest()
        {
            _mockRepo = new Mock<IPersonService>();
            _controller = new PersonsController(_mockRepo.Object);

            persons = new List<Person>()
            {
                new Person
                {
                    UUID=1,
                    Name="Ahmet",
                    Surname="Mehmet",
                    Company="Rise"
                },
                new Person
                {
                    UUID=2,
                    Name="Mehmet",
                    Surname="Ahmet",
                    Company="Test"
                }
            };
        }

        [Fact]
        public async void GetPersons_ActionRun_ReturnOkResultWithPerson()
        {
            _mockRepo.Setup(x => x.GetPersons()).ReturnsAsync(persons);

            var result = await _controller.GetPersons();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPersons = Assert.IsType<List<Person>>(okResult.Value);

            Assert.Equal<int>(2, returnPersons.Count);
        }
        [Theory]
        [InlineData(1)]
        public void UpdatePerson_IdIsNotEqualPerson_ReturnBadRequest(int id)
        {
            var person = GetPerson(id);

            var result = _controller.UpdatePerson(2, person);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void UpdatePerson_ActionRun_ReturnNoContent(int id)
        {
            var person = GetPerson(id);
            _mockRepo.Setup(x => x.UpdatePerson(person));

            var result = _controller.UpdatePerson(id, person);

            _mockRepo.Verify(x => x.UpdatePerson(person), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void AddPerson_ActionRun_ReturnOkResult()
        {
            var person = persons.First();
            _mockRepo.Setup(x => x.AddPerson(person)).Returns(Task.CompletedTask);

            var result = await _controller.AddPerson(person);

            var resultOk = Assert.IsType<OkObjectResult>(result);

            _mockRepo.Verify(x => x.AddPerson(person), Times.Once);

        }

        [Theory]
        [InlineData(1)]
        public async void DeletePerson_ActionRun_IdIsNotEqualPerson_ReturnNotFoundRequest(int id)
        {
            Person person = null;
            _mockRepo.Setup(x => x.GetPersonById(id)).ReturnsAsync(person);

            var resultNotFound = await _controller.DeletePerson(id);

            Assert.IsType<NotFoundResult>(resultNotFound);
        }
        [Theory]
        [InlineData(1)]
        public async void DeletePerson_ActionRun_ReturnNoContent(int id)
        {
            var person = GetPerson(id);
            _mockRepo.Setup(x => x.GetPersonById(id)).ReturnsAsync(person);
            _mockRepo.Setup(x => x.DeletePerson(id));

            var result = await _controller.DeletePerson(id);

            _mockRepo.Verify(x => x.DeletePerson(id), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        private Person GetPerson(int id)
        {
            return persons.First(x => x.UUID == id);
        }
    }
}
