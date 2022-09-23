using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WavShareServiceModels.ApiResponses
{
    public class ApiValidationErrorResult: ObjectResult
    {
        public ApiValidationErrorResult(ModelStateDictionary modelState) 
            : base (new ApiErrorResponse(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
