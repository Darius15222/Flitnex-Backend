using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Repositories;
using FlitnexApi.Validations.Genres;
using FluentValidation;

namespace FlitnexApi.Services;

public class GenreService
{
    private readonly GenreRepository _genreRepository;
    // private readonly Mapper _mapper;

    public GenreService(/*Mapper mapper,*/ GenreRepository genreRepository)
    {
        // _mapper = mapper;
        _genreRepository = genreRepository;
    }

    public DGenre GetGenreById(int id)
    {
        var eGenre = _genreRepository.GetGenreById(id);
        // return _mapper.Map<EGenre, DGenre>(eGenre);
        return AutoMapper.Mapper.Instance.Map<EGenre, DGenre>(eGenre);
    }
    
    public DPaginationList<DGenre> GetAllGenres(DPagination pagination)
    {
        var genresPagination = _genreRepository.GetAllGenres(pagination);
        
        var dGenres = AutoMapper.Mapper.Instance.Map<List<EGenre>, List<DGenre>>(genresPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(genresPagination.Pagination);
        
        
        // return _mapper.Map<List<EGenre>, List<DGenre>>(genres);
        return new DPaginationList<DGenre>
        {
            Items = dGenres,
            Pagination = dPagination
        };
    }
    
    public DPaginationList<DGenre> GetGenresByTerm(string term, DPagination pagination)
    {
        var genresPagination = _genreRepository.GetGenresByTerm(term, pagination);
        
        var dGenres = AutoMapper.Mapper.Instance.Map<List<EGenre>, List<DGenre>>(genresPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(genresPagination.Pagination);
        
        
        // return _mapper.Map<List<EGenre>, List<DGenre>>(genres);
        return new DPaginationList<DGenre>
        {
            Items = dGenres,
            Pagination = dPagination
        };
    }
    
    public DGenre AddGenre(DGenre genreDto)
    {
        var validator = new CreateGenreValidation();
        validator.ValidateAndThrow(genreDto);
        
        // var eGenre = _mapper.Map<DGenre, EGenre>(genreDto);
        var eGenre = AutoMapper.Mapper.Instance.Map<DGenre, EGenre>(genreDto);
        
        var genre = _genreRepository.AddGenre(eGenre);
        
        // var dGenre = _mapper.Map<EGenre, DGenre>(genre);
        var dGenre = AutoMapper.Mapper.Instance.Map<EGenre, DGenre>(genre);
        
        return dGenre;
    }

    public DGenre UpdateGenre(DGenre genreDto)
    {
        var validator = new UpdateGenreValidation();
        validator.ValidateAndThrow(genreDto);
        
        // var eGenre = _mapper.Map<DGenre, EGenre>(genreDto);
        var eGenre = AutoMapper.Mapper.Instance.Map<DGenre, EGenre>(genreDto);
        
        var genre = _genreRepository.UpdateGenre(eGenre);
        
        // var dGenre = _mapper.Map<EGenre, DGenre>(genre);
        var dGenre = AutoMapper.Mapper.Instance.Map<EGenre, DGenre>(genre);
        return dGenre;
    }
    
    public DGenre DeleteGenre(int id)
    {
        var validator = new DeleteGenreValidation();

        var obiect = new DGenre
        {
            Id = id
        };
        
        validator.ValidateAndThrow(obiect);
        
        var genre = _genreRepository.DeleteGenre(id);
        var dGenre = AutoMapper.Mapper.Instance.Map<EGenre, DGenre>(genre);
        return dGenre;
    }

}