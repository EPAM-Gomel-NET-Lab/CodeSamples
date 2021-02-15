using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using LibraryForTests;
using LibraryForTests.Domain;
using LibraryForTests.Services;
using NUnit.Framework;

namespace IntegrationTest
{
    [TestFixture]
    public class BookFileStoreTests
    {
        private TestBookFileStorageSettings _settings;

        [SetUp]
        public void Init()
        {
            _settings = new TestBookFileStorageSettings();
        }

        [TearDown]
        public void RevertStorage()
        {
            string result;
            using (var stream = Assembly.GetAssembly(typeof(ILibrary)).GetManifestResourceStream("LibraryForTests.Books_for_test.txt"))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            File.WriteAllText(_settings.FileNameData, result);
        }
        
        [Test]
        public void GetAll_WhenBookExist_ShouldReturnBooksList()
        {
            // Arrange            
            var bookFileStorage = new BookFileStorage(_settings);

            // Act
            var books = bookFileStorage.GetAll();

            // Assert
            books.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings" },
                new Book { Id = 2, Name = "Le Petit Prince" },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone" },
                new Book { Id = 4, Name = "The Hobbit" },
            });
        }

        [Test]
        public void Add_WhenAddBook_ShouldReturnBooksListWithNewBook()
        {
            // Arrange
            var bookToAdd = new Book { Name = "Herbert Schildt C#" };
                        
            var bookFileStorage = new BookFileStorage(_settings);

            // Act
            bookFileStorage.Add(bookToAdd);
            var books = bookFileStorage.GetAll();

            // Assert
            books.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings" },
                new Book { Id = 2, Name = "Le Petit Prince" },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone" },
                new Book { Id = 4, Name = "The Hobbit" },
                new Book { Id = 5, Name = "Herbert Schildt C#" }
            });
        }

        [Test]
        public void Add_WhenBookIsNull_ShouldReturnException()
        {
            // Arrange    
            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            Action testAction = () => bookFileStorage.Add(null);

            //Assert
            testAction.Should().Throw<InvalidOperationException>()
                .WithMessage("Can not add null object!");
            ////var ex = Assert.Throws<InvalidOperationException>(testAction);
            ////Assert.That(ex.Message, Is.EqualTo(""));
        }


        [Test]
        public void Delete_WhenDeleteExistBook_ShouldReturnBookListWithoutDeletedBook()
        {
            // Arrange
            var bookToDelete = new Book { Id = 4, Name = "The Hobbit" };

            var bookFileStorage = new BookFileStorage(_settings);

            // Act
            var booksBeforeDelete = bookFileStorage.GetAll();
            bookFileStorage.Delete(bookToDelete);
            var booksAfterDelete = bookFileStorage.GetAll();

            // Assert
            booksBeforeDelete.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings" },
                new Book { Id = 2, Name = "Le Petit Prince" },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone" },
                new Book { Id = 4, Name = "The Hobbit" }
            });
            booksAfterDelete.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings" },
                new Book { Id = 2, Name = "Le Petit Prince" },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone" },
            });
        }

        [Test]
        public void Delete_WhenDeleteNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 5, Name = "Herbert Schildt C#", IsArchive = false };

            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Delete(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void Delete_WhenDeleteBookIsNull_ShouldReturnException()
        {
            // Arrange            
            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Delete(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not delete null object!"));
        }

        [Test]
        public void sUpdate_WhenUpdateExistBook_ShouldReturnBookListWithUpdatedBook()
        {
            // Arrange
            var bookToDelete = new Book { Id = 1, Name = "The Lord of the Rings" };

            var bookFileStorage = new BookFileStorage(_settings);

            // Act
            bookFileStorage.Update(bookToDelete);
            var books = bookFileStorage.GetAll();

            // Assert
            books.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 2, Name = "Le Petit Prince" },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone" },
                new Book { Id = 4, Name = "The Hobbit" },
                new Book { Id = 1, Name = "The Lord of the Rings" }
            });
        }

        [Test]
        public void Update_WhenUpdateNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 5, Name = "Herbert Schildt C#", IsArchive = false };
            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Update(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void Update_WhenUpdateBookIsNull_ShouldReturnException()
        {
            // Arrange
            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Update(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not update null object!"));
        }

        [Test]
        public void Archive_WhenArchiveExistBook_ShouldReturnBookListWithArchivedBook()
        {
            // Arrange
            var book = new Book { Id = 1, Name = "The Lord of the Rings" };

            var bookFileStorage = new BookFileStorage(_settings);

            // Act
            bookFileStorage.Archive(book);
            var result = bookFileStorage.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = true },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
                new Book { Id = 4, Name = "The Hobbit" , IsArchive = false }
            });
        }

        [Test]
        public void Archive_WhenArchiveNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 5, Name = "Herbert Schildt C#" };

            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Archive(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void Archive_WhenArchiveBookIsNull_ShouldReturnException()
        {
            // Arrange
            var bookFileStorage = new BookFileStorage(_settings);

            //Act
            TestDelegate testAction = () => bookFileStorage.Archive(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not archive null object!"));
        }
    }
}
