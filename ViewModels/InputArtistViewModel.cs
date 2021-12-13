using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Validation;

namespace MoviesApp.ViewModels

{
    public class InputArtistViewModel
    {
        [CustomArtistValidation(4)]
        [Required]
        [StringLength(32,ErrorMessage = "FirstName length can't be more than 32.")]
        public string FirstName { get; set; }
        
        [CustomArtistValidation(4)]
        [Required]
        [StringLength(32,ErrorMessage = "LastName length can't be more than 32.")]
        public string LastName { get; set; }
        
        [DataType(DataType.Date)] 
        public DateTime BirthdayDate { get; set; }
        
    }
}
