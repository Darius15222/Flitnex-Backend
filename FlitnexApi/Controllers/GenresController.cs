using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FlitnexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GenresController : Controller
{

    private readonly GenreService _genreService;
    
    public GenresController(GenreService genreService)
    {
        _genreService = genreService;
    }
    
    [HttpGet]
    public ActionResult<DPaginationList<DGenre>> GetAllGenres([FromQuery] DPagination pagination)
    {
        var genrePagination = _genreService.GetAllGenres(pagination);

        return Ok(genrePagination);
    }
    
    [HttpGet("search/{term}")]
    public ActionResult<DPaginationList<DGenre>> GetGenresByTerm(string term, [FromQuery] DPagination pagination)
    {
        var genrePagination = _genreService.GetGenresByTerm(term, pagination);

        return Ok(genrePagination);
    }
    
    [HttpGet("{id}")]
    public ActionResult<DGenre> GetGenreById(int id)
    {
        var genre = _genreService.GetGenreById(id);

        return Ok(genre);
    }

    [HttpPost]
    public ActionResult AddGenre([FromBody] DGenre genreDto)
    {
        try
        {
            var genre = _genreService.AddGenre(genreDto);
            return Ok(genre);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        
        
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateGenre(int id, [FromBody] DGenre updatedGenreDto)
    {
        try
        {
            updatedGenreDto.Id = id;
            var genre = _genreService.UpdateGenre(updatedGenreDto);

            return Ok(genre);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        try
        {
            var deleteGenre = _genreService.DeleteGenre(id);
            return Ok(deleteGenre);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    
}