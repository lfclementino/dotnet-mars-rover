using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarsRover.Domain.Helper
{
    public static class ObjectValidation
    {
        public static void Validate<T>(this object obj, string message) where T : Exception
        {
            var isValid = Validator.TryValidateObject(obj,
                                               new ValidationContext(obj),
                                               new List<ValidationResult>(),
                                               true);

            if (!isValid) throw (Exception)Activator.CreateInstance(typeof(T), message);
        }
    }
}
