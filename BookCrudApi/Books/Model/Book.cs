using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCrudApi.Books.Model
{
    [Table("book")]
    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; } 

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("author")]
        public string Author { get; set; }

        [Required]
        [Column("genre")]
        public string Genre { get; set; }

    }
}
