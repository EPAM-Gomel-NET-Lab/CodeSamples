using System.Collections.Generic;
using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    public class BookMemoryStorage : MemoryBaseStorage<Book>, IStore<Book>
    {     
        public BookMemoryStorage(List<Book> entities) : base(entities)
        {
        }               
    }
}
