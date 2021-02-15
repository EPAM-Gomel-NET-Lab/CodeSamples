using System.Collections.Generic;
using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    class UserBookMemoryStorage : MemoryBaseStorage<UserBook>, IStore<UserBook>
    {
        public UserBookMemoryStorage(List<UserBook> entities) : base(entities)
        {
        }
    }
}
