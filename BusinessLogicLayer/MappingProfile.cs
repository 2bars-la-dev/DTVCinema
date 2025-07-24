using AutoMapper;
using BusinessLogicLayer.DTOs;
using Models;

namespace BusinessLogicLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Province, ProvinceDto>().ReverseMap();

            CreateMap<Theatre, TheatreGetDto>()
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province != null ? src.Province.Name : null));
            CreateMap<Theatre, TheatreUpcrateDto>().ReverseMap();

            CreateMap<Screen, ScreenGetDto>()
                .ForMember(dest => dest.TheatreName, opt => opt.MapFrom(src => src.Theatre != null ? src.Theatre.Name : null));
            CreateMap<Screen, ScreenUpcrateDto>().ReverseMap();

            CreateMap<Movie, MovieGetDto>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.PosterUrl != null ? $"https://localhost:7216/images/movie/{src.PosterUrl}" : null));
            CreateMap<Movie, MovieUpcrateDto>().ReverseMap();

            CreateMap<Showtime, ShowtimeGetDto>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie != null ? src.Movie.Name : null))
                .ForMember(dest => dest.ScreenName, opt => opt.MapFrom(src => src.Screen != null ? src.Screen.Name : null))
                .ForMember(dest => dest.TheatreName, opt => opt.MapFrom(src => src.Screen != null && src.Screen.Theatre != null ? src.Screen.Theatre.Name : null));
            CreateMap<Showtime, ShowtimeUpcrateDto>().ReverseMap();
        }
    }
}