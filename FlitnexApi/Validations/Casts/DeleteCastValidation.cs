using FlitnexApi.Dtos.MovieDto;
using FlitnexApi.Repositories;
using FluentValidation;

namespace FlitnexApi.Validations.Casts;

public class DeleteCastValidation : AbstractValidator<DCast>
{
    public DeleteCastValidation(CastRepository castRepository, int movieId)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(id => castRepository.GetCastById(movieId, id) != null)
            .WithMessage("Cast with the provided movieId and Id does not exist in the database!");
    }
}