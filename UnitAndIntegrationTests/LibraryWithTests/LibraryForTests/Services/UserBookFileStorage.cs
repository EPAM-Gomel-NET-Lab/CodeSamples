using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    class UserBookFileStorage : FileStorageBase<UserBook>, IStore<UserBook>
    {
        public UserBookFileStorage(IFIleStorageSettings settings) : base(settings)
        {
        }    
    }
}
