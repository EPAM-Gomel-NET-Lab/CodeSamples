using ClientApp;
using IdentityCreation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using static IdentityCreation.Controllers.PersonsController;

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
            var controller = new PersonsController(Mock.Of<IFunctor>());
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            };
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            // Act
            var result = controller.Index();
            var viewResult = result as ViewResult;

            // Asssert
            var model = viewResult.Model as Person;
            Assert.That(model.FirstName, Is.EqualTo("John"));
            Assert.That(model.LastName, Is.EqualTo("Doe"));
        }
    }
}