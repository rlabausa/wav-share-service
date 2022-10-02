using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.AudioFiles;
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

            // This will duplicate error message if applied with [Required]
            //if (string.IsNullOrEmpty(guidOption))
            //{
            //    return new ValidationResult($"{Header.ClientCorrelId} header is required.");
            //}

            if (guidOption != ClientCorrelIdHeaderValue.Generate)
            {
                Guid guid;
                bool isValidGuid = Guid.TryParse(guidOption, out guid);

                if (!isValidGuid)
                {
                    return new ValidationResult($"{nameof(ApiRequestHeaders.ClientCorrelId)} value must be 'generate' or a valid GUID format.");
                }
            }

            return ValidationResult.Success;
        }
    }

}
