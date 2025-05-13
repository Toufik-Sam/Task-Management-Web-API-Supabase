using FluentValidation;
using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;

namespace TaskManagementAPI.InputValidation.AuthControllerValidation;

public class SignUpDataValidator:AbstractValidator<SignUpDataDTO>
{
    public SignUpDataValidator()
    {
        RuleFor(User => User.FirstName)
            .NotEmpty() .WithMessage("FirstName is Required !")
            .Matches("^[a-zA-Z ]+$").WithMessage("Name Must contain only Letters and Spaces!")
            .MaximumLength(50).WithMessage("Name Too long Max Length should be 50 charachters!");

        RuleFor(User => User.LastName)
            .NotEmpty().WithMessage("FirstName is Required !")
            .Matches("^[a-zA-Z ]+$").WithMessage("Name Must contain only Letters and Spaces!")
            .MaximumLength(50).WithMessage("Name Too long Max Length should be 50 charachters!");

        RuleFor(User => User.Email)
            .NotEmpty().WithMessage("Email Must Be Non Empty !")
            .EmailAddress().WithMessage("Invalid email format !")
            .MaximumLength(320).WithMessage("Invalid Email!");

        RuleFor(User => User.Phone)
            .NotEmpty().WithMessage("Phone Number is Required !")
            .Matches(@"^\d{10}$").WithMessage("Invalid Format Phone number must be exactly 10 digits!");

        RuleFor(User => User.Password)
            .NotEmpty().WithMessage("Password is required !")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>_\-+=\\[\];:/`~]+").WithMessage("Password must contain at least one special character.");
    }
}
