using System.Threading.Tasks;
using Gameboard.Controllers.Api;
using Gameboard.MetaModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Gameboard_Tests.Api
{
    public class CompanyApiControllerTests
    {
        [Fact]
        public async Task Index_CreateAnInvalidCompany()
        {
            var mockRepo = new Mock<ICompanyRepository>();
            var model = new CompanyModel { Name = "Fake" };
            mockRepo.Setup(x => x.Context.Create(model)).Returns(Task.FromResult(null));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Post(model);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task Index_CreateAValidCompany()
        {
            var mockRepo = new Mock<ICompanyRepository>();
            var model = new CompanyModel {Name = "Company1"};
            mockRepo.Setup(x => x.Context.Create(model)).Returns(Task.FromResult((Company) model));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Post(model);
            var newModel = ((OkObjectResult) actionResult).Value as Company;

            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(model.Name, newModel.Name);
        }

        [Fact]
        public async Task Index_GetsAnExistingCompany()
        {
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(x => x.Context.Get("Company1")).Returns(Task.FromResult(new Company {Id = "Company1"}));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Get("Company1");
            var model = ((OkObjectResult) actionResult).Value as Company;

            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("Company1", model.Id);
        }

        [Fact]
        public async Task Index_GetsAnNonExistingCompany()
        {
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(x => x.Context.Get("Company1")).Returns(Task.FromResult(new Company {Id = "Company1"}));

            var controller = new CompaniesController(mockRepo.Object);
            var actionResult = await controller.Get("Company2");

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        //    var mockRepo = new Mock<ICompanyRepository>();
        //{
        //public async Task Index_UpdateAValidCompany()

        //[Fact]

        //    var controller = new CompaniesController(mockRepo.Object);
        //    var model = (CompanyModel) new Company {Name = "Company1"} ;
        //    IActionResult actionResult = await controller.Post(model);
        //    var newModel = ((OkObjectResult)actionResult).Value as Company;

        //    Assert.IsType<OkObjectResult>(actionResult);
        //    Assert.Equal(model.Name, newModel.Name);
        //}

        //[Fact]
        //public async Task Index_UpdateAnInvalidCompany()
        //{
        //    var mockRepo = new Mock<ICompanyRepository>();

        //    var controller = new CompaniesController(mockRepo.Object);
        //    var model = (CompanyModel) new Company { Name = "Fake"} ;
        //    IActionResult actionResult = await controller.Post(model);

        //    Assert.IsType<BadRequestResult>(actionResult);
        //}
    }
}