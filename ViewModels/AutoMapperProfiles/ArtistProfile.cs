using MoviesApp.Models;
using AutoMapper;
using MoviesApp.Services.Dto;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class ArtistProfile: Profile
    {
        public ArtistProfile()
        {
            CreateMap<ArtistDto, InputArtistViewModel>().ReverseMap();
            CreateMap<ArtistDto, DeleteArtistViewModel>();
            CreateMap<ArtistDto, EditArtistViewModel>().ReverseMap();
            CreateMap<ArtistDto, ArtistViewModel>();
            CreateMap<ArtistDto, ViewFilmsViewModel>().ReverseMap();
            CreateMap<ArtistDto, ArtistDtoApi>().ReverseMap();

        }
    }
}