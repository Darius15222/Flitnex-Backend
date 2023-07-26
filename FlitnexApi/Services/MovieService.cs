using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Repositories;
using FlitnexApi.Validations.Movies;
using FluentValidation;

namespace FlitnexApi.Services;

public class MovieService
{
    private readonly MovieRepository _movieRepository;
    
    public MovieService(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public DMovie GetMovieById(int id)
    {
        var eMovie = _movieRepository.GetMovieById(id);

        return AutoMapper.Mapper.Instance.Map<EMovie, DMovie>(eMovie);
    }
    
    public DPaginationList<DMovie> GetAllMovies(DPagination pagination)
    {
        var moviesPagination = _movieRepository.GetAllMovies(pagination);
        
        var dMovies = AutoMapper.Mapper.Instance.Map<List<EMovie>, List<DMovie>>(moviesPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(moviesPagination.Pagination);

        return new DPaginationList<DMovie>
        {
            Items = dMovies,
            Pagination = dPagination
        };
    }
    
    public DPaginationList<DMovie> GetMoviesByTerm(string term, DPagination pagination)
    {
        var moviesPagination = _movieRepository.GetMoviesByTerm(term, pagination);
        
        var dMovies = AutoMapper.Mapper.Instance.Map<List<EMovie>, List<DMovie>>(moviesPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(moviesPagination.Pagination);
        
        
        // return _mapper.Map<List<EGenre>, List<DGenre>>(genres);
        return new DPaginationList<DMovie>
        {
            Items = dMovies,
            Pagination = dPagination
        };
    }
    
    public DMovie AddMovie(DMovieUpdateParams movieUpdateParams)
    {
        var validator = new CreateMovieValidation();
        validator.ValidateAndThrow(movieUpdateParams);
        
        
        var eMovie = AutoMapper.Mapper.Instance.Map<DMovieUpdateParams, EMovie>(movieUpdateParams);
        
        var movie = _movieRepository.AddMovie(eMovie);
        
        // var dMovie = _mapper.Map<EMovie, DMovie>(Movie);
        var dMovie = AutoMapper.Mapper.Instance.Map<EMovie, DMovie>(movie);
        
        return dMovie;
    }
    
    public DMovie UpdateMovie(DMovieUpdateParams movieUpdateParams)
    {
        var validator = new UpdateMovieValidation();
        validator.ValidateAndThrow(movieUpdateParams);
        
        var eMovie = AutoMapper.Mapper.Instance.Map<DMovieUpdateParams, EMovie>(movieUpdateParams);
        
        var movie = _movieRepository.UpdateMovie(eMovie);
        
        var dMovie = AutoMapper.Mapper.Instance.Map<EMovie, DMovie>(movie);
        return dMovie;
    }
    
    public DMovie DeleteMovie(int id)
    {
        var validator = new DeleteMovieValidation();

        var obiect = new DMovie
        {
            Id = id
        };
        
        validator.ValidateAndThrow(obiect);
        
        var movie = _movieRepository.DeleteMovie(id);
        var dMovie = AutoMapper.Mapper.Instance.Map<EMovie, DMovie>(movie);
        return dMovie;
    }

}