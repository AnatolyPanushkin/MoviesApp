using MoviesApp.Models;
using AutoMapper;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class ArtistProfile: Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, InputArtistViewModel>().ReverseMap();
            CreateMap<Artist, DeleteArtistViewModel>();
            CreateMap<Artist, EditArtistViewModel>().ReverseMap();
            CreateMap<Artist, ArtistViewModel>();
        }
    }
}