using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FlitnexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController : Controller
{

    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public ActionResult<DPaginationList<DMovie>> GetAllMovies([FromQuery] DPagination pagination)
    {
        var moviesPagination = _movieService.GetAllMovies(pagination);
        return Ok(moviesPagination);
    }
    
    [HttpGet("search/{term}")]
    public ActionResult<DPaginationList<DMovie>> GetMoviesByTerm(string term, [FromQuery] DPagination pagination)
    {
        var moviesPagination = _movieService.GetMoviesByTerm(term, pagination);

        return Ok(moviesPagination);
    }

    [HttpGet("{id}")]
    public ActionResult<DMovie> GetMovieById(int id)
    {
        var movie = _movieService.GetMovieById(id);
        
        return Ok(movie);
    }
    
    [HttpPost]
    public ActionResult<DMovie> AddMovie(DMovieUpdateParams movieUpdateParams) 
    {
        try
        {
            var movie = _movieService.AddMovie(movieUpdateParams);

            return Ok(movie);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<DMovie> UpdateMovie([FromRoute] int id, [FromBody] DMovieUpdateParams movieUpdateParams)
    {
        try
        {
            movieUpdateParams.Id = id;
            var movie = _movieService.UpdateMovie(movieUpdateParams);

            return Ok(movie);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        try
        {
            var deletedMovie = _movieService.DeleteMovie(id);
            return Ok(deletedMovie);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
}