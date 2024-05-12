using FluentValidation;
using LocaMoto.Application.DTOs.Requests;

namespace LocaMoto.Application.Validator
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator()
        {
            RuleFor(x=> x.UserEmail)
                .NotEmpty()
                .WithMessage("The filed UserEmail can't be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}|:<>?-])[a-zA-Z\d!@#$%^&*()_+{}|:<>?-]{8,}$")
                .WithMessage("Password must contain at least 8 characters, including one letter, one number, and one symbol.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("The filed Role can't be empty");
        }
    }
}
