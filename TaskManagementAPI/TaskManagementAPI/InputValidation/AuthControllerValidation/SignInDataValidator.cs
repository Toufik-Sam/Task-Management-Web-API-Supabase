using FluentValidation;
using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;

namespace TaskManagementAPI.InputValidation.AuthControllerValidation;

public class SignInDataValidator : AbstractValidator<SignInDataDTO>
{
    public SignInDataValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email Must Be Non Empty !")
           .EmailAddress().WithMessage("Invalid email format !")
           .MaximumLength(320).WithMessage("Invalid Email!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required !")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>_\-+=\\[\];:/`~]+").WithMessage("Password must contain at least one special character.");
    }
}
