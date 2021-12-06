
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Library.DataAccessSQL.Entities
{
    [Table("Books")]
    public class BookEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<AuthorEntity> Authors { get; set; }
    }
}
