using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Library.Contracts.Book
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<int> AuthorsIds { get; set; }
    }
}
