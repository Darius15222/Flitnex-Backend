using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Casts;

public class MovieIdValidation : AbstractValidator<DMovie>
{
    private readonly MovieRepository _movieRepository = new();

    public MovieIdValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("MovieId must not be null!")
            .NotEmpty().WithMessage("MovieId must not be empty!")
            .Must(id => _movieRepository.GetMovieById(id) != null).WithMessage("Movie with the provided id does not exist in the database!");
    }
}