using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using LibraryForTests;
using LibraryForTests.Domain;
using LibraryForTests.Services;
using Moq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class LibraryTests
    {
        [Test]
        public void Rent_WhenUserRentMoreTwoBooks_ShouldReturnException()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5},
                new UserBook {BookId = 3, UserId = 4},
                new UserBook {BookId = 7, UserId = 4},
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            TestDelegate testAction = () => library.RentBook(new User {Id = 4}, new Book {Id = 8});

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not rent more than two Books!"));
        }


        [Test]
        public void Rent_WhenUserIsNull_ShouldReturnException()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5},
                new UserBook {BookId = 3, UserId = 4},
                new UserBook {BookId = 7, UserId = 4},
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            TestDelegate testAction = () => library.RentBook(null, new Book { Id = 3 });
                        
            // Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Null user can not rent Book!"));
        }


        [Test]
        public void Rent_WhenUserRentsNullBook_ShouldReturnException()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5},
                new UserBook {BookId = 3, UserId = 4},
                new UserBook {BookId = 7, UserId = 4},
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            TestDelegate testAction = () => library.RentBook(new User { Id = 4 }, null);

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not rent null Book!"));
        }


        [Test]
        public void RentBook_WhenRentBook_NewUserBookAdded()
        {
            // Arrange
            var userBookToAdd = new UserBook { BookId = 7, UserId = 7 };
            var userBooks = new List<UserBook>();

            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(userBooks);
            userBooksMock.Setup(m => m.Add(It.IsAny<UserBook>())).Callback<UserBook>(ub => userBooks.Add(ub));
            
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            library.RentBook(new User { Id = 7}, new Book { Id = 7 } );

            // Assert
            userBooks.Should()
                .ContainSingle(ub => ub.UserId == 7 && ub.BookId == 7 && ub.RentDateStart >= DateTime.Now.AddSeconds(-1));
            userBooks.Should().BeEquivalentTo(new List<UserBook> 
            {
                new UserBook { UserId = 7, BookId = 7 }
            }, options => options.Excluding(x => x.RentDateStart));
        }

        [Test]
        public void RentBook_WhenRentBook_NewUserBookAddedWithStartTimeRent()
        {
            // Arrange
            var userBookToAdd = new UserBook { BookId = 7, UserId = 7 };
            var userBooks = new List<UserBook>();

            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(userBooks);
            userBooksMock.Setup(m => m.Add(It.IsAny<UserBook>())).Callback<UserBook>(ub => userBooks.Add(ub));

            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            library.RentBook(new User { Id = 7 }, new Book { Id = 7 });

            // Assert
            var _userBokkTimeStart = userBooks.Find(ub => ub.UserId == 7 && ub.BookId == 7).RentDateStart;
            _userBokkTimeStart.Should().BeCloseTo(DateTime.Now);
        }


        [Test]
        public void ReturnBook_WhenReturnNonExistingUserBook_ShouldReturnException()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5},
                new UserBook {BookId = 3, UserId = 4},
                new UserBook {BookId = 7, UserId = 4},
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            Action testAction = () => library.ReturnBook(new User { Id = 2 }, new Book { Id = 3 });

            // Assert
            testAction.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("There is no book for this user in the UserBook Storage!");
        }

        [Test]
        public void ReturnBookHistory_WhenReturnUserBookIsNull_ShouldReturnException()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5},
                new UserBook {BookId = 3, UserId = 4},
                new UserBook {BookId = 7, UserId = 4},
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            TestDelegate testAction = () => library.ReturnBookHistory(null);

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(testAction);
            Assert.That(ex.Message, Is.EqualTo("Can not return history of null Book!"));
        }

        [Test]
        public void ReturnBookHistory_WhenReturnUserBookHistory_ShouldReturnAllUserBookHistory()
        {
            // Arrange 
            var userBooksMock = new Mock<IStore<UserBook>>();
            var userStoreMock = new Mock<IStore<User>>();
            var bookStoreMock = new Mock<IStore<Book>>();
            userBooksMock.Setup(m => m.GetAll()).Returns(new List<UserBook>
            {
                new UserBook {BookId = 1, UserId = 5, IsArchive = true },
                new UserBook {BookId = 3, UserId = 4, IsArchive = false },
                new UserBook {BookId = 7, UserId = 4, IsArchive = true },
                new UserBook {BookId = 7, UserId = 6, IsArchive = true },
            });
            var library = new Library(userStoreMock.Object, bookStoreMock.Object, userBooksMock.Object);

            // Act
            var result = library.ReturnBookHistory(new Book { Id = 7 } );

            // Assert
            result.Should().BeEquivalentTo(new List<UserBook>
            {
                new UserBook {BookId = 7, UserId = 4, IsArchive = true },
                new UserBook {BookId = 7, UserId = 6, IsArchive = true }
            });
        }
    }
}
