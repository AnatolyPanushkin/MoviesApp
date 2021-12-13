using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.ViewModels
{
    public class InputMovieViewModel
    {
        [Required]
        [StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}