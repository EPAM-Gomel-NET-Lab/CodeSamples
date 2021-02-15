using System;
using System.Collections.Generic;
using FluentAssertions;
using LibraryForTests.Domain;
using LibraryForTests.Services;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class BookMemoryStorageTests
    {
        [Test]
        public void GivenGetAll_WhenBooksExist_ShouldReturnBookList()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var store = new BookMemoryStorage(books);

            // Act
            var result = store.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            });
        }

        [Test]
        public void GivenGetAll_WhenBooksNotProvide_ShouldReturnEmptyList()
        {
            // Arrange
            var store = new BookMemoryStorage(null);

            // Act
            var result = store.GetAll();

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GivenAdd_WhenAddBook_ShouldReturnBooksListWithNewBook()
        {
            // Arrange
            var book = new Book { Id = 4, Name = "Herbert Schildt C#", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };           
            var bookMemoryStorage = new BookMemoryStorage(books);

            // Act
            bookMemoryStorage.Add(book);
            var result = bookMemoryStorage.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
                new Book { Id = 4, Name = "Herbert Schildt C#", IsArchive = false }
            });
        }

        [Test]
        public void GivenAdd_WhenBookIsNull_ShouldReturnException()
        {
            //Arrange            
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Add(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not add null object!"));
        }

        [Test]
        public void GivenDelete_WhenDeleteExistBook_ShouldReturnBookListWithoutDeletedBook()
        {
            // Arrange
            var book = new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            // Act
            bookMemoryStorage.Delete(book);
            var result = bookMemoryStorage.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false }
            });
        }

        [Test]
        public void GivenDelete_WhenDeleteNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 4, Name = "Herbert Schildt C#", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Delete(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void GivenDelete_WhenDeleteBookIsNull_ShouldReturnException()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Delete(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not delete null object!"));
        }

        [Test]
        public void GivenUpdate_WhenUpdateExistBook_ShouldReturnBookListWithUpdatedBook()
        {
            // Arrange
            var book = new Book { Id = 1, Name = "Herbert Schildt C#", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            // Act
            bookMemoryStorage.Update(book);
            var result = bookMemoryStorage.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
                new Book { Id = 1, Name = "Herbert Schildt C#", IsArchive = false },
            });
        }

        [Test]
        public void GivenUpdate_WhenUpdateNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 4, Name = "Herbert Schildt C#", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Update(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void GivenUpdate_WhenUpdateBookIsNull_ShouldReturnException()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Update(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not update null object!"));
        }

        [Test]
        public void GivenArchive_WhenArchiveExistBook_ShouldReturnBookListWithArchivedBook()
        {
            // Arrange
            var book = new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            // Act
            bookMemoryStorage.Archive(book);
            var result = bookMemoryStorage.GetAll();

            // Assert
            result.Should().BeEquivalentTo(new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = true },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false }
            });
        }

        [Test]
        public void GivenArchive_WhenArchiveNonExistBook_ShouldReturnException()
        {
            // Arrange
            var book = new Book { Id = 4, Name = "Herbert Schildt C#", IsArchive = false };
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Archive(book);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("There is not this item in the Item Storage!"));
        }

        [Test]
        public void GivenArchive_WhenArchiveBookIsNull_ShouldReturnException()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "The Lord of the Rings", IsArchive = false },
                new Book { Id = 2, Name = "Le Petit Prince", IsArchive = false },
                new Book { Id = 3, Name = "Harry Potter and the Philosopher's Stone", IsArchive = false },
            };
            var bookMemoryStorage = new BookMemoryStorage(books);

            //Act
            TestDelegate testAction = () => bookMemoryStorage.Archive(null);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not archive null object!"));
        }

    }
}
