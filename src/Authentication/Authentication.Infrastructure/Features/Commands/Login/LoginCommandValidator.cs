﻿using FluentValidation;

namespace Authentication.Infrastructure.Features.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{Email} is required.")
                .EmailAddress().WithMessage("{Email} is not a valid email address.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{Password} is required.");
        }
    }
}