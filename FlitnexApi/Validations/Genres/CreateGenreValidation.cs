using FlitnexApi.Dtos;
using FlitnexApi.Dtos.MovieDto;
using FluentValidation;

namespace FlitnexApi.Validations.Genres;

public class CreateGenreValidation : AbstractValidator<DGenre>
{
    public CreateGenreValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
        // .NotEqual("");
    }
}