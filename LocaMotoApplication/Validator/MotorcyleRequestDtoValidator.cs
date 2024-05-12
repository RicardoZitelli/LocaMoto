using FluentValidation;
using LocaMotoApplication.DTOs.Requests;


namespace LocaMotoApplication.Validator
{
    public class MotorcyleRequestDtoValidator: AbstractValidator<MotorcycleRequestDto>
    {
        public MotorcyleRequestDtoValidator() 
        {
            RuleFor(x=>x.Model)
                .NotEmpty()
                .WithMessage("Model can't be empty")
                .MaximumLength(50)
                .WithMessage("Model can't be longer than 50 characters");

            RuleFor(x => x.LicensePlate)
                .NotEmpty()
                .WithMessage("License plate can't be empty")
                .MaximumLength(50)
                .WithMessage("License plate can't be longer than 50 characters");

            RuleFor(x => x.Year)
                .GreaterThan(1900)
                .WithMessage("Year can't be less than 1900");               
        }
    }
}
