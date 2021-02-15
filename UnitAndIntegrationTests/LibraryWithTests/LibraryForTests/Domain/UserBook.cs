using System;

namespace LibraryForTests.Domain
{
    public class UserBook : IHasBasicId, IArchivable
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime RentDateStart { get; set; }

        public DateTime RentDateEnd { get; set; }

        public bool IsArchive { get; set; }
    }
}
