using System.Globalization;
using dotnet_rpg.Application.Dtos.Character;
using FluentValidation;

namespace dotnet_rpg.Application.Validations.Character;

public class GetCharacterResponseDtoValidator: AbstractValidator<GetCharacterResponseDto>
{
    public GetCharacterResponseDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
        RuleFor(c => c.Name)
            .Length(1, 50)
            .NotEmpty();
        RuleFor(c => c.HitPoints)
            .GreaterThan(0)
            .NotEmpty();
        RuleFor(c => c.Class)
            .IsInEnum()
            .NotEmpty()
            .NotNull();
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");
    }
}