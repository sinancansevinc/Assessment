using Assessment.Controllers;
using Assessment.Dtos;
using Assessment.Models;
using Assessment.Services;
using Assessment.ViewModels;
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
    public class ContactsControllerTest
    {
        private readonly Mock<IContactService> _mockRepo;
        private readonly ContactsController _controller;

        private List<Contact> contacts;
        private List<ContactCreateDto> contactCreateDtos;
        private List<ContactDto> contactListDtos;


        public ContactsControllerTest()
        {
            _mockRepo = new Mock<IContactService>();
            _controller = new ContactsController(_mockRepo.Object);

            contacts = new List<Contact>()
            {
                new Contact
                {
                    ContactContent="123456789",
                    TypeId=1,
                    Id=1,
                    PersonId=1

                },
                new Contact
                {
                    ContactContent="ssinancan@gmail.com",
                    TypeId=2,
                    PersonId=2,
                    Id=2

                }
            };

            contactCreateDtos = new List<ContactCreateDto>()
            {
                new ContactCreateDto
                {
                    ContactTypeId=1,
                    PersonId=1,
                    ContactContent="02124444400"
                },
                new ContactCreateDto
                {
                    ContactTypeId=3,
                    PersonId=2,
                    ContactContent="Ankara"

                }
            };
            contactListDtos = new List<ContactDto>()
             {
                 new ContactDto
                 {
                     Name="Sinancan",
                     Surname="Sevinç",
                     Company="SSCompany",
                     Type="Phone",
                     Content="12345677889"
                 },
                 new ContactDto
                 {
                     Name="Sinancan",
                     Surname="Sevinç",
                     Company="SSCompany",
                     Type="Location",
                     Content="İstanbul"
                 },
             };

        }

        [Fact]
        public async void AddContactInformation_ActionRun_ReturnOkResult()
        {
            var contactInformation = contactCreateDtos.First();
            _mockRepo.Setup(x => x.AddContactInformation(contactInformation)).Returns(Task.CompletedTask);

            var result = await _controller.AddContactInformation(contactInformation);

            var resultOk = Assert.IsType<OkObjectResult>(result);

            _mockRepo.Verify(x => x.AddContactInformation(contactInformation), Times.Once);

        }

        [Theory]
        [InlineData(1)]
        public async void DeleteContact_ActionRun_IdIsNotEqualContact_ReturnNotFoundRequest(int id)
        {
            Contact contact = null;
            _mockRepo.Setup(x => x.GetContactInformationById(id)).ReturnsAsync(contact);

            var resultNotFound = await _controller.DeleteContactInformation(id);

            Assert.IsType<NotFoundResult>(resultNotFound);
        }
        [Theory]
        [InlineData(1)]
        public async void DeleteContact_ActionRun_ReturnNoContent(int id)
        {
            var contact = GetContact(id);
            _mockRepo.Setup(x => x.GetContactInformationById(id)).ReturnsAsync(contact);
            _mockRepo.Setup(x => x.DeleteContactInformation(id));

            var result = await _controller.DeleteContactInformation(id);

            _mockRepo.Verify(x => x.DeleteContactInformation(id), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async void GetContactInformationsByPersonId_ActionRun_PersonIsNotEqual_ReturnNotFoundRequest(int personId)
        {
            List<ContactDto> contactListDto = null;
            _mockRepo.Setup(x => x.GetContactInformationsByPersonId(personId)).ReturnsAsync(contactListDto);
            var resultNotFound = await _controller.GetContactInformationsByPersonId(personId);
            Assert.IsType<NotFoundResult>(resultNotFound);
        }
        [Theory]
        [InlineData(1)]
        public async void GetContactInformationsByPersonId_ActionRun_ReturnOkResultObject(int personId)
        {
            var cListDtos = contactListDtos;
            _mockRepo.Setup(x => x.GetContactInformationsByPersonId(personId)).ReturnsAsync(cListDtos);

            var result = await _controller.GetContactInformationsByPersonId(personId);
            var resultOk = Assert.IsType<OkObjectResult>(result);

            _mockRepo.Verify(x => x.GetContactInformationsByPersonId(personId), Times.Once);

        }

        private Contact GetContact(int id)
        {
            return contacts.First(x => x.Id == id);
        }
    }
}
