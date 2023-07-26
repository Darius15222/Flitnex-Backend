using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Genres;

public class UpdateGenreValidation : AbstractValidator<DGenre>
{
    private readonly GenreRepository _genreRepository = new();
    public UpdateGenreValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().Must(id => _genreRepository.GetGenreById(id) != null);
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}