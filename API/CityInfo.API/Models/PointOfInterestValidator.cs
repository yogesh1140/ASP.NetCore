using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationValidator : AbstractValidator<PointOfInterestForCreationDto>
    {
        public PointOfInterestForCreationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("You should provide value for name");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Description should have less than 50 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("You should provide value for Description");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("Description should have less than 200 characters");
            RuleFor(x => x.Description).NotEqual(x => x.Name).WithMessage("Description should bedifferent from the name");
        }

    }
}
