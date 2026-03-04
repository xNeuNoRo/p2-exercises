using App.Application.DTOs.Base;
using FluentValidation;

namespace App.Application.Validators.Base;

public class ProfileRequestBaseValidator<T> : AbstractValidator<T>
    where T : ProfileRequestBase
{
    public ProfileRequestBaseValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del estudiante es obligatorio.");

        RuleFor(x => x.Career).NotEmpty().WithMessage("La carrera del estudiante es obligatoria.");

        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("La matricula del estudiante es obligatoria.");
    }
}
