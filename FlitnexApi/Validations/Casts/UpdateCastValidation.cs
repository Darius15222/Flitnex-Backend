using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Casts;

public class UpdateCastValidation : AbstractValidator<DCast>
{
    public UpdateCastValidation(CastRepository castRepository, int movieId)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(id => castRepository.GetCastById(movieId, id) != null)
            .WithMessage("Cast with the provided movieId and Id does not exist in the database!");

        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Role)
            .NotNull()
            .NotEmpty();
    }
}