using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services.ArtistServices;
using MoviesApp.Services.Dto;

namespace MoviesApp.Controllers.ApiControllers
{
        [Route("api/artist")]
        [ApiController]
        public class ArtistApiController:ControllerBase
        {
            private readonly IArtistService _service;
        
            public ArtistApiController(IArtistService service)
            {
                _service = service;
            }

            [HttpGet] // GET: /api/movies
            [ProducesResponseType(200, Type = typeof(IEnumerable<ArtistDto>))]  
            [ProducesResponseType(404)]
            public ActionResult<IEnumerable<ArtistDto>> GetArtists()
            {
                return Ok(_service.GetAllArtistApi());
            }
        
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(ArtistDto))]  
            [ProducesResponseType(404)]
            public IActionResult GetById(int id)
            {
                var artist = _service.GetArtistApi(id);
                if (artist == null) return NotFound();  
                return Ok(artist);
            }
        
            [HttpPost] 
            public ActionResult<ArtistDto> PostArtist(ArtistDtoApi inputDto)
            {
                var artist= _service.AddArtistApi(inputDto);
                return CreatedAtAction("GetById", new { id = artist.Id }, artist);
            }
        
            [HttpPut("{id}")] 
            public IActionResult UpdateArtist(int id, ArtistDtoApi editDto)
            {
                var artist = _service.UpdateArtistApi(editDto);

                if (artist==null)
                {
                    return BadRequest();
                }

                return Ok(artist);
            }
        
            [HttpDelete("{id}")] 
            public ActionResult<ArtistDto> DeleteArtist(int id)
            {
                var artist = _service.DeleteMoviesArtists(id);
                if (artist == null) return NotFound();  
                return Ok(artist);
            }
        }
    }
