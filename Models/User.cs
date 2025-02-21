using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    [Table("Users")]
    public class User
    {

        [Column("user_id", TypeName = "int")]
        [Key]
        public int Id { get; set; }

        [Column("username", TypeName = "varchar")]
        [Required]
        [StringLength(125)]
        public string UserName { get; set; }

        [Column("email", TypeName = "varchar")]
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("mobile_no", TypeName = "varchar")]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Column("password", TypeName = "varchar")]
        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [Column("createdAt", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column("role", TypeName = "varchar")]
        [Required]
        [StringLength(10)]
        public string Role { get; set; } = "Seller";

        //[Column("isUserLogged", TypeName = "bit")]
        //[Required]
        //[StringLength(10)]
        //public short isUserLogged { get; set; } = 0;
    }

    public class BookShopDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True;Encrypt=True;");
        }
    }

}
