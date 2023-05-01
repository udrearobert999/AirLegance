﻿using Application.Dto;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    private readonly IUserService _userService;

    public UserRegistrationDtoValidator(IUserService userService)
    {
        _userService = userService;
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must have at least two characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must have at least two characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(EmailNotInUse).WithMessage("Email already in use!");
    }

    private async Task<bool> EmailNotInUse(string email, CancellationToken cancellationToken)
    {
        bool emailExists = await _userService.EmailExists(email);
        return !emailExists;
    }
}