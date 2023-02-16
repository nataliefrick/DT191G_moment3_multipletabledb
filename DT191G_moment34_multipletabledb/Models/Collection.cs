using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DT191G_moment34_multipletabledb.Models
{
    public class Collection
    {
        // properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionId { get; set; }

        public string? Artist { get; set; }

        [Display(Name = "Album Title")]
        public string? AlbumTitle { get; set; }

        [Display(Name = "Release Year")]
        public string? ReleaseYear { get; set; }

        [Display(Name = "Song List")]

        public string? SongList { get; set; }

        [NotMapped]
        public int Borrowed { get; set; }
        [NotMapped]
        public string Friend { get; set; } = "";

    }
}
