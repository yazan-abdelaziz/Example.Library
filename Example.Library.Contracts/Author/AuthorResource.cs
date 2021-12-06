using System.Collections.Generic;

namespace Example.Library.Contracts.Author
{
    public class AuthorResource
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<BookLightResource> Books { get; set; }
    }
}
