using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WavShareServiceBLL;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.ApiResponses;
using WavShareServiceModels.AudioFiles;
using WavShareServiceModels.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavShareService.Controllers
{
    /// <summary>
    /// API Controller for Audio Files.
    /// </summary>
    public class AudioFilesController : ApiControllerBase
    {
        private IAudioFileBLL _audioFileBLL;
        private ILogger<AudioFilesController> _logger;

        /// <summary>
        /// Constructs an AudioFilesController.
        /// </summary>
        /// <param name="audioFileBLL"></param>
        /// <param name="logger"></param>
        public AudioFilesController(IAudioFileBLL audioFileBLL, ILogger<AudioFilesController> logger)
        {
            _audioFileBLL = audioFileBLL;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve audio file details
        /// </summary>
        /// <param name="requestHeaders"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(GetAudioFilesDetailsResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Get([FromQuery] ApiRequestHeaders requestHeaders, [FromQuery] GetAudioFilesRequest requestParams)
        {
            var results = await _audioFileBLL.GetAudioFilesDetails(requestParams);
            return Ok(results);
        }


        /// <summary>
        /// Retrieve audio file with encoded audio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AudioFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _audioFileBLL.GetAudioFileById(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);

        }

        /// <summary>
        /// Create new audio file
        /// </summary>
        /// <param name="requestHeaders"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(AudioFileDetails), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromQuery] ApiRequestHeaders requestHeaders, [FromBody] CreateAudioFileRequest requestBody)
        {
            var newAudioFileDetails = await _audioFileBLL.CreateAudioFile(requestBody);

            if (newAudioFileDetails != null)
            {
                return CreatedAtAction(nameof(Get), new { id = newAudioFileDetails.AudioFileId }, newAudioFileDetails);
            }
            else
            {
                //TODO: Customize POST error response
                return BadRequest();
            }
        }

        /// <summary>
        /// Update existing audio file
        /// </summary>
        /// <param name="requestHeaders"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] ApiRequestHeaders requestHeaders, [FromBody] UpdateAudioFileRequest requestBody)
        {
            var updateSuccessful = await _audioFileBLL.UpdateAudioFile(requestBody);

            if (updateSuccessful)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete existing audio file
        /// </summary>
        /// <param name="requestHeaders"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ApiRequestHeaders requestHeaders, [FromQuery] DeleteAudioFileRequest requestParams)
        {
            var deleteSuccessful = await _audioFileBLL.DeleteAudioFile(requestParams);

            if (deleteSuccessful)
            {
                return NoContent();
            }
            else
            {
                //TODO: Customize DELETE error response
                return BadRequest();
            }

        }
    }
}
