using FluentValidation;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator  : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p=>p.CarModelName).NotEmpty();
            RuleFor(p=>p.Description).NotEmpty();
            RuleFor(p=>p.ModelYear).NotEmpty();
            RuleFor(p=>p.DailyPrice).NotEmpty();

            RuleFor(p => p.CarModelName).MinimumLength(2);
            RuleFor(p=>p.DailyPrice).GreaterThan(0);

        }
    }
}
