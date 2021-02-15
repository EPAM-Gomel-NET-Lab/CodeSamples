using LibraryForTests.Domain;

namespace LibraryForTests
{
    public interface ILibrary
    {
        void RentBook(User user, Book book);

        void ReturnBook(User user, Book book);

        UserBook[] ReturnBookHistory(Book book);
    }    
}
