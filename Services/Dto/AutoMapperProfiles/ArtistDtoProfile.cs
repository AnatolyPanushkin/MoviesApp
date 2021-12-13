using System.Linq;
using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Services.Dto.AutoMapperProfiles
{
    public class ArtistDtoProfile:Profile
    {
        public ArtistDtoProfile()
        {
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<ViewFilmsViewModel, ArtistDto>().ReverseMap();


            CreateMap<Artist, ArtistDtoApi>().ForMember(e => e.MoviesArtistsIds,
                    opt =>
                        opt.MapFrom(m => m.MoviesArtists.Select(a => a.MovieId)))
                .ReverseMap()
                .ForMember(e => e.MoviesArtists,
                    opt => opt.Ignore());
            
            CreateMap<ArtistDto, ArtistDtoApi>().ForMember(e => e.MoviesArtistsIds,
                arg => arg.MapFrom(
                    opt => opt.SelectOptions.Select(int.Parse)));
        }
    }
}