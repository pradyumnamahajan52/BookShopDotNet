using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    [Table("Books")]
    public class Book
    {
        [Column("book_id", TypeName = "int")]
        [Key]
        public int Id { get; set; }

        [Column("book_name", TypeName = "varchar")]
        [Required]
        [StringLength(250)]
        public string BookName { get; set; }

        [Column("author", TypeName = "varchar")]
        [Required]
        [StringLength(250)]
        public string Author { get; set; }


        [Column("price", TypeName = "int")]
        [Required]
        public int Price { get; set; }

        [Column("category", TypeName = "varchar")]
        [StringLength(120)]
        public string Category { get; set; }

        [Column("user_id", TypeName = "int")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Foreign key property

        public User User { get; set; } // Navigation property

        [Column("createdAt", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
    }
}

