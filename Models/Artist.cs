using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace MoviesApp.Models
{
    public class Artist
    {
        public Artist()
        {
            MoviesArtists = new HashSet<MoviesArtist>();
        }
       
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [DataType(DataType.Date)] 
        public DateTime BirthdayDate { get; set; }
        
        
        public virtual ICollection<MoviesArtist> MoviesArtists { get; set; }
       
    }
}