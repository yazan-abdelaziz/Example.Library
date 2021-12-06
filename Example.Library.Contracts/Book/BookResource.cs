using System.Collections.Generic;

namespace Example.Library.Contracts.Book
{
    public class BookResource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<AuthorLightResource> Authors { get; set; }
    }
}
