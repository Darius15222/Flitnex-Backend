using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Entities;
using FlitnexApi.Repositories;
using FlitnexApi.Validations.Casts;
using FluentValidation;

namespace FlitnexApi.Services;

public class CastService
{
    private readonly CastRepository _castRepository;

    public CastService(CastRepository castRepository)
    {
        _castRepository = castRepository;
    }
    
    public DCast GetCastById(int movieId, int id)
    {
        var eCast = _castRepository.GetCastById(movieId, id);
        return AutoMapper.Mapper.Instance.Map<ECast, DCast>(eCast);
    }
    
    public DPaginationList<DCast> GetAllCasts(int movieId, DPagination pagination)
    {
        var castsPagination = _castRepository.GetAllCasts(movieId, pagination);
        
        var dCasts = AutoMapper.Mapper.Instance.Map<List<ECast>, List<DCast>>(castsPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(castsPagination.Pagination);
        
        
        return new DPaginationList<DCast>
        {
            Items = dCasts,
            Pagination = dPagination
        };
    }
    
    public DPaginationList<DCast> GetCastsByTerm(string term, int movieId, DPagination pagination)
    {
        var castsPagination = _castRepository.GetCastsByTerm(term, movieId, pagination);
        
        var dCasts = AutoMapper.Mapper.Instance.Map<List<ECast>, List<DCast>>(castsPagination.Items);
        var dPagination = AutoMapper.Mapper.Instance.Map<DPagination>(castsPagination.Pagination);
        
        return new DPaginationList<DCast>
        {
            Items = dCasts,
            Pagination = dPagination
        };
    }
    
    public DCast AddCast(int movieId, DCast castDto)
    {
        var idValidator = new MovieIdValidation();
        var movie = new DMovie
        {
            Id = movieId
        };
        idValidator.ValidateAndThrow(movie);
        
        var validator = new CreateCastValidation();
        validator.ValidateAndThrow(castDto);
        
        var eCast = AutoMapper.Mapper.Instance.Map<DCast, ECast>(castDto);
        
        var cast = _castRepository.AddCast(movieId, eCast);
        
        var dCast = AutoMapper.Mapper.Instance.Map<ECast, DCast>(cast);
        
        return dCast;
    }
    
    public DCast UpdateCast(int movieId, DCast castDto)
    {
        var idValidator = new MovieIdValidation();
        var movie = new DMovie
        {
            Id = movieId
        };
        idValidator.ValidateAndThrow(movie);
        
        var validator = new UpdateCastValidation(_castRepository, movieId);
        validator.ValidateAndThrow(castDto);
        
        var eCast = AutoMapper.Mapper.Instance.Map<DCast, ECast>(castDto);

        var cast = _castRepository.UpdateCast(movieId, eCast);
        
        var updateCast = AutoMapper.Mapper.Instance.Map<ECast, DCast>(cast);
        return updateCast;
    }
    
    public DCast DeleteCast(int movieId, int id)
    {
        var idValidator = new MovieIdValidation();
        var movie = new DMovie
        {
            Id = movieId
        };
        idValidator.ValidateAndThrow(movie);
        
        var validator = new DeleteCastValidation(_castRepository, movieId);

        var obiect = new DCast
        {
            Id = id
        };
        
        validator.ValidateAndThrow(obiect);
        
        var cast = _castRepository.DeleteCast(movieId, id);
        var dCast = AutoMapper.Mapper.Instance.Map<ECast, DCast>(cast);
        return dCast;
    }
}