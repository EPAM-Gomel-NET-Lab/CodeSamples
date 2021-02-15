using System.Collections.Generic;
using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    public interface IStore<T>
        where T : IHasBasicId, IArchivable
    {
        void Add(T item);

        List<T> GetAll();

        void Delete(T item);

        void Update(T item);

        void Archive(T item);
    }
}
