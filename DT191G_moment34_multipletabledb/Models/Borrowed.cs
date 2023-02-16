using DT191G_moment34_multipletabledb.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DT191G_moment34_multipletabledb.Models
{
    public class Borrowed
    {
        // intersection table

        // properties
        [Key]
        public int BorrowedId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } //date borrowed


        [ForeignKey("Collection")]
        //[Display(Name = "Collection I")]
        public int CollectionId { get; set; } //id of the album borrowed
        [ForeignKey("Friends")]
        public int FriendId { get; set; } //id of the friend who borrowed


        public Friends Friend { get; set; }
        public Collection Collection { get; set; }

    }
}

