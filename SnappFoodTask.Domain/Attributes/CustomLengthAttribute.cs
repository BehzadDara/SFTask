using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnappFoodTask.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomLengthAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;
        public CustomLengthAttribute(int min = int.MinValue, int max = int.MaxValue)
        {
            _min= min;
            _max= max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((value as string)!.Length > _max)
            {
                return new ValidationResult($"Length of value must be under {_max}.");
            }
            if ((value as string)!.Length < _min)
            {
                return new ValidationResult($"Length of value must be over {_min}.");
            }

            return ValidationResult.Success;
        }
    }
}
