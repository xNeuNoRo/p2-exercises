using App.Application.DTOs.Base;
using FluentValidation;

namespace App.Application.Validators.Base;

public class PetRequestBaseValidator<T> : AbstractValidator<T>
    where T : PetRequestBase
{
    public PetRequestBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre de la mascota es obligatorio.")
            .MaximumLength(100)
            .WithMessage("El nombre de la mascota no puede exceder los 100 caracteres.");

        RuleFor(x => x.Race)
            .NotEmpty()
            .WithMessage("La raza de la mascota es obligatoria.")
            .MaximumLength(100)
            .WithMessage("La raza de la mascota no puede exceder los 100 caracteres.");

        RuleFor(x => x.Species)
            .IsInEnum()
            .WithMessage("La especie de la mascota debe ser un valor válido.");

        RuleFor(x => x.Age)
            .InclusiveBetween(0, 20)
            .WithMessage("La edad de la mascota debe estar entre 0 y 20 años.");
    }
}
