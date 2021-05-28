using ClassicTemplate.Controllers;
using ClassicTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AspNetTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PersonsControllerTest()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>(), Mock.Of<IUserService>());
            var context = new DefaultHttpContext
            {
                Session = Mock.Of<ISession>()
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context,
            };
            controller.TempData = new TempDataDictionary(context, Mock.Of<ITempDataProvider>());

            // Act
            var result = controller.Index();
            var viewResult = result as ViewResult;

            // Asssert
            var model = viewResult.Model as User;
            Assert.That(model.FirstName, Is.EqualTo("John"));
            Assert.That(model.LastName, Is.EqualTo("Doe"));
        }
    }
}