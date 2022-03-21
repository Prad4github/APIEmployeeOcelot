using EmployeeManagement.Model;
using FluentValidation;

namespace EmployeeManagement.Validator
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotNull()
                .WithMessage("Please Povide First Name");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
