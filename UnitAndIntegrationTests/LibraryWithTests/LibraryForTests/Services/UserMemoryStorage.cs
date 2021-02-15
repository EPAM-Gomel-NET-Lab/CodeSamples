using System.Collections.Generic;
using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    class UserMemoryStorage : MemoryBaseStorage<User>, IStore<User>
    {
        public UserMemoryStorage(List<User> entities) : base(entities)
        {
        }
    }
}
