using Microsoft.AspNetCore.Mvc;
using WavShareServiceModels.ApiResponses;
using WavShareServiceModels.Constants;

namespace WavShareService.Controllers
{


    /// <summary>
    /// Custom base class for API Controllers to inherit.
    /// </summary>
    
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]

    [Produces(Mime.MediaTypes.Json)]
    [Consumes(Mime.MediaTypes.Json)]
    
    [Route("api/[controller]")]
    [ApiController]

    public class ApiControllerBase : ControllerBase
    {

    }

}
