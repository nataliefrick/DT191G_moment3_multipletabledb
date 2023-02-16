using System.ComponentModel.DataAnnotations;

namespace DT191G_moment34_multipletabledb.Models
{
    public class Friends
    {
        // properties
        [Key]
        public int FriendId { get; set; }
        [Required]
        public string Name { get; set; } //name of the person borrowing the album
        public string? Email { get; set; }


        // connects Friend table with Borrowed table
        public ICollection<Borrowed>? Borrowed { get; set; }
    }
}

