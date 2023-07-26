using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Movies;

public class DeleteMovieValidation : AbstractValidator<DMovie>
{
    private readonly MovieRepository _movieRepository = new();
    public DeleteMovieValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().Must(id => _movieRepository.GetMovieById(id) != null);
    }
}