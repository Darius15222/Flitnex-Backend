using FlitnexApi.Dtos.MovieDto;
using FluentValidation;

namespace FlitnexApi.Validations.Casts;

public class CreateCastValidation : AbstractValidator<DCast>
{
    public CreateCastValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Role)
            .NotNull()
            .NotEmpty();
    }
}