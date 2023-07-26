using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Movies;

public class UpdateMovieValidation : AbstractValidator<DMovieUpdateParams>
{
    private readonly MovieRepository _movieRepository = new();
    private readonly GenreRepository _genreRepository = new();
    public UpdateMovieValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().Must(id => _movieRepository.GetMovieById(id) != null);

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