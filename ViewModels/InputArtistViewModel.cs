using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Validation;

namespace MoviesApp.ViewModels

{
    public class InputArtistViewModel
    {
        [CustomArtistValidation(4)]
        public string FirstName { get; set; }
        
        [CustomArtistValidation(4)]
        public string LastName { get; set; }
        
        [DataType(DataType.Date)] 
        public DateTime BirthdayDate { get; set; }
        
    }
}
