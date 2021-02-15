namespace LibraryForTests.Domain
{
    public class Book : IHasBasicId, IArchivable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsArchive { get; set; }
    }
}
