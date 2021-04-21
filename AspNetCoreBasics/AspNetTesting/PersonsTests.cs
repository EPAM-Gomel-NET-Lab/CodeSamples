using ClientApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetTesting
{
    public class PersonsTests : IntegrationTestBase
    {
        [Test]
        public async Task Index_WhenPersonExists_ShoudlReturnSuccess()
        {
            // Arrange & Act
            var result = await Client.GetAsync("person1");
            var service = Provider.GetService(typeof(IFunctor)) as IFunctor;

            // Assert
            service.DoWork();
            var content = await result.Content.ReadAsStringAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Is.EqualTo("John Doe"));
        }
    }
}
