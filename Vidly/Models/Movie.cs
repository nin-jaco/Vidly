using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Razor;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; } = new DateTime();

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        [Range(1,20)]
        public byte GenreId { get; set; }

        public byte NumberAvailable { get; set; }
    }
}