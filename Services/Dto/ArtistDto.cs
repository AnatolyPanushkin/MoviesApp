using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MoviesApp.Models;
using MoviesApp.Validation;

namespace MoviesApp.Services.Dto
{
    public class ArtistDto
    {
            public int? Id { get; set; }
            
            [Required]
            [StringLength(32,ErrorMessage = "FirstName length can't be more than 32.")]
            
            public string FirstName { get; set; }
            
            [Required]
            [StringLength(32,ErrorMessage = "LastName length can't be more than 32.")]
            public string LastName { get; set; }
        
        
            [Required]
            [DataType(DataType.Date)] 
            public DateTime BirthdayDate { get; set; }
            
            [Required]
            public string movieName { get; set; }
            
            public virtual ICollection<MoviesArtist> MoviesArtists { get; set; }
        
            public ICollection<string> SelectOptions { get; set; }
            
        
    }
}