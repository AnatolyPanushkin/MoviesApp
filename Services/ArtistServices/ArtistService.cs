using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels;

namespace MoviesApp.Services.ArtistServices
{
    public class ArtistService:IArtistService
    {
        private readonly MoviesContext _context;
        private readonly IMapper _mapper;
        
        public ArtistService(MoviesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public ArtistDto GetArtist(int id)
        {
            return _mapper.Map<ArtistDto>(_context.Artists.FirstOrDefault(m => m.Id == id));
        }

        public IEnumerable<ArtistDto> GetAllArtists()
        {
            return _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDto>>(_context.Artists.ToList());
        }

        public ArtistDto UpdateArtist(ArtistDto artistDto)
        {
            if (artistDto.Id == null)
            {
                return null;
            }
            try
            {
                var artist = _mapper.Map<Artist>(artistDto);
                
                _context.Update(artist);
                _context.SaveChanges();
                
                return _mapper.Map<ArtistDto>(artist);
            }
            catch (DbUpdateException)
            {
                if (!ArtistExists((int) artistDto.Id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public ArtistDto AddArtist(ArtistDto artistDto)
        {
            var artist = _context.Add((object) _mapper.Map<Artist>(artistDto)).Entity;
            _context.SaveChanges();
            return _mapper.Map<ArtistDto>(artist);
        }

        public ArtistDto DeleteArtist(int id)
        {
            var artist = _context.Artists.Find(id);
            if (artist == null)
            {
                return null;
            }

            _context.Artists.Remove(artist);
            _context.SaveChanges();
            
            return _mapper.Map<ArtistDto>(artist);
        }
        
        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }

        public IEnumerable<ArtistDto> MovieArtist(int id)
        {
            var result = _mapper.Map<IEnumerable<ViewFilmsViewModel>, IEnumerable<ArtistDto>>(_context.MoviesArtists.Where(m => m.ArtistId == id).Select(m => new ViewFilmsViewModel
            {
                movieName = m.Movie.Title
            }).ToList());
            return result;
        }

        public ArtistDto DeleteMoviesArtists(int id)
        {
            var artist = _context.Artists.Find(id);
            foreach (var v in _context.MoviesArtists) {
                if (v.ArtistId == id) {
                    _context.MoviesArtists.Remove(v);
                }
            }
            _context.Artists.Remove(artist);
            _context.SaveChanges();

            return _mapper.Map<ArtistDto>(artist);
        }

        #region Api
            
         public IEnumerable<ArtistDtoApi> GetAllArtistApi()
        {
            return _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDtoApi>>(
                _context.Artists.Include(e => e.MoviesArtists).ToList()
            );
        }

        public ArtistDtoApi GetArtistApi(int id)
        {
            return _mapper.Map<ArtistDtoApi>(
                _context.Artists
                    .Include(e => e.MoviesArtists)
                    .FirstOrDefault(a => a.Id == id)
            );
            
            
        }

        public ArtistDtoApi AddArtistApi(ArtistDtoApi inputDtoApi)
        {
            var artist = _context.Artists.Add(_mapper.Map<Artist>(inputDtoApi)).Entity;
            _context.SaveChanges();
            if (inputDtoApi.MoviesArtistsIds != null)
            {
                foreach (var movie in inputDtoApi.MoviesArtistsIds)
                {
                    var id = _mapper.Map<ArtistDto>(artist).Id;
                    if (id != null)
                    {
                        var movieToAdd = new MoviesArtist
                        {
                            ArtistId = (int) id,
                            MovieId = movie
                        };
                        _context.MoviesArtists.Add(movieToAdd);
                    }
                }
            }

            _context.SaveChanges();
            return _mapper.Map<ArtistDtoApi>(artist);
        }

        public ArtistDtoApi UpdateArtistApi(ArtistDtoApi artistDto)
        {
            if (artistDto.Id == null)
            {
                return null;
            }
            try
            {
                var artist = _mapper.Map<Artist>(artistDto);
                
                _context.Update(artist);
                _context.SaveChanges();
                
                return _mapper.Map<ArtistDtoApi>(artist);
            }
            catch (DbUpdateException)
            {
                if (!ArtistExists((int) artistDto.Id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        

        #endregion
    }
    }
