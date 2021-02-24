using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ConfigurationInsideTests
{
    public class ConfigReaderTests
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers#xml-configuration-provider
        /// 1) Use Microsoft.Extensions.Configuration.Xml nuget package.
        /// 2) Do not name your file like app.config. It will be merged into project properties so you would not upload it into config.
        /// 3) Add CopyToOutputDirectory to Always in order to copy settings into the build artifacts.
        /// </summary>
        [Test]
        public void LoadingFromXmlFile()
        {
            // Arrange
            var configs = new ConfigurationBuilder().AddXmlFile("appsettings.config").Build();

            // Act
            var valueFromConfig = configs["connectionString:value"];

            // Assert
            Assert.That(valueFromConfig, Is.EqualTo("ConnectionStringFromXml"));
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers#json-configuration-provider
        /// 1) Use Microsoft.Extensions.Configuration.Json nuget package.
        /// 2) Add CopyToOutputDirectory to Always in order to copy settings into the build artifacts.
        /// </summary>
        [Test]
        public void LoadingFromJsonFile()
        {
            // Arrange
            var configs = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            // Act
            var valueFromConfig = configs["ConnectionStrings:DatabaseForTests"];

            // Assert
            Assert.That(valueFromConfig, Is.EqualTo("ConnectionStringFromJsonFile"));
        }
    }
}
