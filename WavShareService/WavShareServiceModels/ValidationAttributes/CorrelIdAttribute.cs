using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.Constants;

namespace WavShareServiceModels.ValidationAttributes
{
    public class CorrelIdAttribute : ValidationAttribute
    {
        public CorrelIdAttribute()
        {

        }
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            string? guidOption = (string)value ?? null;

            if (string.IsNullOrEmpty(guidOption))
            {
                return new ValidationResult($"{Header.ClientCorrelId} header is required.");
            }

            if (guidOption != ClientCorrelIdHeaderValue.Generate)
            {
                Guid guid;
                bool isValidGuid = Guid.TryParse(guidOption, out guid);

                if (!isValidGuid)
                {
                    return new ValidationResult($"{Header.ClientCorrelId} value must be 'generate' or a valid GUID format.");
                }
            }

            return ValidationResult.Success;
        }
    }

}
