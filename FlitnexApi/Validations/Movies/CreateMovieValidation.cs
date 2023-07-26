using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Movies;

public class CreateMovieValidation : AbstractValidator<DMovieUpdateParams>
{
    private readonly GenreRepository _genreRepository = new();
    public CreateMovieValidation()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.GenreIds)
            .ForEach(e =>
            {
                e.Must(id => _genreRepository.GetGenreById(id) != null);
            })
            .NotNull()
            .NotEmpty();
    }
}