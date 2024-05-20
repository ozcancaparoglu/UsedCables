using FluentValidation;

namespace Authentication.Infrastructure.Features.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Roles)
                .NotNull()
                .NotEmpty()
                .WithMessage("Roles cannot be null or empty");
        }
    }
}