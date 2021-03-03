using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u=>u.FirstName).NotEmpty();
            RuleFor(u=>u.LastName).NotEmpty();
            RuleFor(u=>u.Email).NotEmpty();
            

            RuleFor(u => u.FirstName).MinimumLength(2).WithMessage("İsim uzunluğu en az 2 karakter olabilir.");
            RuleFor(u => u.LastName).MinimumLength(2).WithMessage("Soyisim uzunluğu en az 2 karakter olabilir.");
            


        }
    }
}
