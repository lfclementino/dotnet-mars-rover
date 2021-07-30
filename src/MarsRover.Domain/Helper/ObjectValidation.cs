using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarsRover.Domain.Helper
{
    public static class ObjectValidation
    {
        public static void Validate(this object obj, Exception exception = null)
        {
            var isValid = Validator.TryValidateObject(obj,
                                               new ValidationContext(obj),
                                               new List<ValidationResult>(), 
                                               true);

            if (!isValid) throw exception ?? new Exception($"Invalid value");
        }
    }
}
