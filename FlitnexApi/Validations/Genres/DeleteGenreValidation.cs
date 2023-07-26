using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Genres;

public class DeleteGenreValidation : AbstractValidator<DGenre>
{
    private readonly GenreRepository _genreRepository = new();
    
    public DeleteGenreValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().Must(id => _genreRepository.GetGenreById(id) != null);
    }
}