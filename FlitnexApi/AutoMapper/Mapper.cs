using AutoMapper;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;

namespace FlitnexApi.AutoMapper;

public class Mapper
{
    public static IMapper Instance { get; } = Configure();
    
    private static IMapper Configure()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DMovie, EMovie>().ReverseMap();
            cfg.CreateMap<DGenre, EGenre>().ReverseMap();
            cfg.CreateMap<DCast, ECast>().ReverseMap();

            // cfg.CreateMap<DMovieUpdateParams, EMovie>().ReverseMap();
            // cfg.CreateMap<GenreIds, EGenre>().ReverseMap();

            cfg.CreateMap<DMovieUpdateParams, EMovie>()
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src 
                        => src.GenreIds.Select(id => new EGenre { Id = id }).ToList()));
            

            cfg.CreateMap(typeof(DPaginationList<>), typeof(DPaginationList<>));
            cfg.CreateMap<DPagination, DPagination>().ReverseMap();
            
        }).CreateMapper();
    }
}