using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Library.Contracts.Book
{
    public class BookModel
    {
        [Required]
        public string Title { get; set; }

        public List<int> AuthorsIds { get; set; }
    }
}
