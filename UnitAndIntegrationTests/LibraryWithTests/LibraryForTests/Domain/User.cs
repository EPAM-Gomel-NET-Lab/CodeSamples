namespace LibraryForTests.Domain
{
    public class User : IHasBasicId, IArchivable
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsArchive { get; set; }
    }
}
