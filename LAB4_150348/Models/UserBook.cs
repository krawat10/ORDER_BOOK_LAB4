using System.ComponentModel.DataAnnotations;

namespace LAB4_150348.Models
{
    public class UserBook
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public bool IsCurrentlyRented { get; set; }
    }
}