using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Column(TypeName = GlobalConstants.AuthorFirstNameType)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = GlobalConstants.AuthorLastNameType)]
        public string LastName { get; set; }
    }
}
