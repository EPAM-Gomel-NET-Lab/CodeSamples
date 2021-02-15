using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    class UserFileStorage : FileStorageBase<User>, IStore<User>
    {
        public UserFileStorage(IFIleStorageSettings settings) : base(settings)
        {
        }
    }
}
