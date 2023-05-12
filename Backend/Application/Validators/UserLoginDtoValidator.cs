using Application.Dto;
using FluentValidation;

namespace Application.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}