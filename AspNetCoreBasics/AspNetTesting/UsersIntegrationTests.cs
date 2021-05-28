using ClassicTemplate.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetTesting
{
    public class UsersIntegrationTests : IntegrationTestBase
    {
        [Test]
        public async Task CreatePerson_WhenPersonValid_ShoudlReturnRedirectAndAddPerson()
        {
            // Arrange & Act
            var formData = new MultipartFormDataContent
            {
                { new StringContent("Ivan"), "FirstName" },
                { new StringContent("Ivanov"), "LastName" }
            };
            var formDataSecond = new MultipartFormDataContent
            {
                { new StringContent("Ivan"), "FirstName" },
                { new StringContent("Taturevich"), "LastName" }
            };
            var service = GetService<IUserService>();
            var result = await Client.PostAsync("persons/add", formData);
            var resultSecond = await Client.PostAsync("persons/add", formDataSecond);

            // Assert
            var people = service.GetAll();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
            Assert.That(resultSecond.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
            Assert.That(people, Is.EquivalentTo(new List<User>
            {
                new User ("Ivan", "Ivanov"),
                new User ("Ivan", "Taturevich")
            }));
        }
    }
}
