using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Validators
{
    public class MeasurementValidator : AbstractValidator<Models.Measurement>
    {
        public MeasurementValidator()
        {
            RuleFor(x => x.Pressure).GreaterThan(0).WithMessage("Pressure must be greater than 0");
            RuleFor(x => x.Pressure).LessThan(16384).WithMessage("Pressure must be less than 16384");
            RuleFor(x => x.SensorID).NotEmpty().WithMessage("SensorID must be specified");
            RuleFor(x => x.SensorID.Length).LessThan(16).WithMessage("SensorID must be less than 16 characters");
            RuleFor(x => x.Temperature).GreaterThan(-32).WithMessage("Temperature must be greater than -32");
            RuleFor(x => x.Temperature).LessThan(256).WithMessage("Temperature must be less than 256");
        }

        public static string[] GetErrors(Models.Measurement measurement)
        {
            if (measurement == null)
            {
                return new string[] { "Could not generate a measurement from the specified body" };
            }

            var validator = new MeasurementValidator();
            var measurementValidationResult = validator.Validate(measurement);
            if (!measurementValidationResult.IsValid)
            {
                return measurementValidationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            }

            return null;
        }
    }
}
