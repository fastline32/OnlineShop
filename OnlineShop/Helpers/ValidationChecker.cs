using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Helpers
{
    public class ValidationChecker
    {
        public static bool IsValid<T>(T dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
