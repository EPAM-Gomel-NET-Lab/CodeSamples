using System;
using System.Linq;
using LibraryForTests.Domain;
using LibraryForTests.Services;

namespace LibraryForTests
{
    public class Library : ILibrary
    {
        private readonly IStore<User> _users;
        private readonly IStore<Book> _books;
        private readonly IStore<UserBook> _userBooks;

        public Library(IStore<User> users, IStore<Book> books, IStore<UserBook> userBooks)
        {
            _users = users;
            _books = books;
            _userBooks = userBooks;
        }

        public void RentBook(User user, Book book)
        {
            if(user == null)
            {
                throw new InvalidOperationException("Null user can not rent Book!");
            }

            if(book == null)
            {
                throw new InvalidOperationException("Can not rent null Book!");
            }
                       

            var canRentBook = _userBooks.GetAll().Count(c => c.UserId == user.Id && !c.IsArchive) < 2;
            if (!canRentBook)
            {
                throw new InvalidOperationException("Can not rent more than two Books!");
            }

            _userBooks.Add(new UserBook { BookId = book.Id, UserId = user.Id, RentDateStart = DateTime.Now  });
        }

        public void ReturnBook(User user, Book book)
        {
            var returnBook = _userBooks.GetAll().Find(x => x.UserId == user.Id && x.BookId == book.Id && !x.IsArchive);
            if(returnBook == null)
            {
                throw new InvalidOperationException("There is not this book for this user in the UserBook Storage!");
            }

            _userBooks.Delete(returnBook);
        }

        public UserBook[] ReturnBookHistory(Book book)
        {
            if (book == null)
            {
                throw new InvalidOperationException("Can not return history of null Book!");
            }
            var bookHistory = _userBooks.GetAll().Where(ub => ub.BookId == book.Id && ub.IsArchive);
            return bookHistory.ToArray();
        }
    }
}
