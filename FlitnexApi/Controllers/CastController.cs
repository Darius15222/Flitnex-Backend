using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Dtos.MoviePaginationDto;
using FlitnexApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FlitnexApi.Controllers;

[ApiController]
[Route("api/v1/movies/{movieId}/[controller]")]
public class CastController : Controller
{
    
    private readonly CastService _castService;

    public CastController(CastService castService)
    {
        _castService = castService;
    }

    [HttpGet]
    public ActionResult<DPaginationList<DCast>> GetAllCasts([FromRoute] int movieId, [FromQuery] DPagination pagination)
    {
        var castPagination = _castService.GetAllCasts(movieId, pagination);

        return Ok(castPagination);
    }
    
    [HttpGet("search/{term}")]
    public ActionResult<DPaginationList<DCast>> GetCastsByTerm([FromRoute]string term, [FromRoute] int movieId, [FromQuery] DPagination pagination)
    {
        var castPagination = _castService.GetCastsByTerm(term, movieId, pagination);

        return Ok(castPagination);
    }
    
    [HttpGet("{id}")]
    public ActionResult<DCast> GetCastById([FromRoute] int movieId, [FromRoute] int id)
    {
        var cast = _castService.GetCastById(movieId, id);

        return Ok(cast);
    }
    
    [HttpPost]
    public ActionResult AddCast([FromBody] DCast castDto, [FromRoute] int movieId)
    {
        try
        {
            var cast = _castService.AddCast(movieId, castDto);

            return Ok(cast);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpPut ("{id}")]
    public ActionResult UpdateCast([FromRoute] int movieId, [FromRoute]int id, [FromBody] DCast updatedCastDto)
    {
        try
        {
            updatedCastDto.Id = id;
            var cast = _castService.UpdateCast(movieId, updatedCastDto);

            return Ok(cast);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCast(int movieId, int id)
    {
        try
        {
            var deletedCast = _castService.DeleteCast(movieId, id);
            return Ok(deletedCast);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    
}