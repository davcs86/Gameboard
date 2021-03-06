﻿using System.Threading.Tasks;
using Gameboard.Controllers.Api;
using Gameboard.MetaModels;
using Gameboard_DAL;
using Gameboard_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Gameboard_Tests.Api
{
    public class CompanyApiControllerTests
    {
        [Fact]
        public async Task Create_WhenModelHasErrors_ReturnsBadRequestError()
        {
            var mockRepo = new Mock<DbContext>();
            var controller = new CompaniesController(mockRepo.Object);
            controller.ModelState.AddModelError("", "Error");

            var actionResult = await controller.Post(new CompanyModel());

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Create_CreatesAValidCompany()
        {
            var mockRepo = new Mock<DbContext>();
            var model = new CompanyModel {Name = "Company1"};
            var mockModel = new Company {Name = "Company1"};
            mockRepo.Setup(x => x.Companies.Create(model)).Returns(Task.FromResult(mockModel));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Post(model);

            var actionResultObj = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(actionResultObj);
            var newModel = Assert.IsType<Company>(actionResultObj.Value);
            Assert.NotNull(newModel.Name);
            Assert.Equal<string>(model.Name, newModel.Name);
        }

        [Fact]
        public async Task Read_GetsAnExistingCompany()
        {
            var mockRepo = new Mock<DbContext>();
            var mockModel = new Company { Id = "Company1" };
            mockRepo.Setup(x => x.Companies.Get("Company1")).Returns(Task.FromResult(mockModel));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Get("Company1");

            var actionResultObj = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(actionResultObj);
            var model = Assert.IsType<Company>(actionResultObj.Value);
            Assert.NotNull(model.Id);
            Assert.Equal<string>("Company1", model.Id);
        }

        [Fact]
        public async Task Read_WhenAsksAnNonExistingCompany_ReturnsNotFoundError()
        {
            var mockRepo = new Mock<DbContext>();
            mockRepo.Setup(x => x.Companies.Get("Company1")).Returns(Task.FromResult(new Company {Id = "Company1"}));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Get("Company2");

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async Task Update_ReturnsTheUpdatedCompany()
        {
            var mockRepo = new Mock<DbContext>();
            var oldModel = new Company { Id = "Company1", Name="Company #1" };
            var updateModel = new Company { Id = "Company1", Name="Company #2" };
            var mockModel = new CompanyModel { Id = "Company1" };

            mockRepo.Setup(x => x.Companies.Get("Company1")).Returns(Task.FromResult(oldModel));
            mockRepo.Setup(x => x.Companies.Update(mockModel)).Returns(Task.FromResult(updateModel));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Put("Company1", mockModel);

            var actionResultObj = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(actionResultObj);

            var updatedModel = Assert.IsType<Company>(actionResultObj.Value);
            Assert.NotNull(updatedModel.Name);
            Assert.Equal<string>(updateModel.Name, updatedModel.Name);
        }

        [Fact]
        public async Task Update_WhenModelHasErrors_ReturnsBadRequestError()
        {
            var mockRepo = new Mock<DbContext>();
            var mockModel = new Company { Id = "Company1" };
            mockRepo.Setup(x => x.Companies.Get("Company1")).Returns(Task.FromResult(mockModel));

            var controller = new CompaniesController(mockRepo.Object);
            controller.ModelState.AddModelError("", "Error");
            var actionResult = await controller.Put("Company1", new CompanyModel());

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Update_WhenModelIsNull_ReturnsBadRequestError()
        {
            var mockRepo = new Mock<DbContext>();
            var mockModel = new Company { Id = "Company1" };
            mockRepo.Setup(x => x.Companies.Get("Company1")).Returns(Task.FromResult(mockModel));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Put("Company1", null);

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}
