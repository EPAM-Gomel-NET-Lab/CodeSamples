using System;
using System.IO;
using LibraryForTests.Services;

namespace IntegrationTest
{
    public class TestBookFileStorageSettings : IFIleStorageSettings
    {
        public string FileNameData 
            => Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Books_for_test.txt");
    }
}
