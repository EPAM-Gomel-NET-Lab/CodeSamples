using System.Collections.Generic;
using System.Linq;
using LibraryForTests.Domain;

namespace LibraryForTests.Core
{
    public static class EntityExtensions
    {
        public static int GetIncrementedId(this IEnumerable<IHasBasicId> entitites)
        {
            return entitites.Select(x => x.Id).Max() + 1;
        }
    }
}
