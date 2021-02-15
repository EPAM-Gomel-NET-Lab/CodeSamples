using System.Collections.Generic;
using LibraryForTests.Domain;

namespace IntegrationTest.Data
{
    public static class UserBooks
    {
        public static List<UserBook> GetUserBooks = new List<UserBook>
        {
            new UserBook { BookId = 4, UserId = 1 },
            new UserBook { BookId = 3, UserId = 1 },
            new UserBook { BookId = 1, UserId = 2 },
            new UserBook { BookId = 3, UserId = 3 },
        };
    }
}
