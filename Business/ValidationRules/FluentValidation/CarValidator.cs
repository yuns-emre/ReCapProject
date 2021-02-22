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
            RuleFor(p=>p.Descriptions).NotEmpty();
            RuleFor(p=>p.Descriptions).MinimumLength(2);
            RuleFor(p=>p.DailyPrice).NotEmpty();
            RuleFor(p=>p.DailyPrice).GreaterThan(0);
        }
    }
}
