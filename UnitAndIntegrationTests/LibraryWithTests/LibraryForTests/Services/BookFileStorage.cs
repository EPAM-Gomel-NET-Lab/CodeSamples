using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    public class BookFileStorage : FileStorageBase<Book>, IStore<Book>
    {    
        public BookFileStorage(IFIleStorageSettings settings) : base(settings)
        {            
        }
    }
}
