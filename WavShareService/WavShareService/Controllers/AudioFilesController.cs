using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WavShareServiceBLL;
using WavShareServiceModels.AudioFiles;
using WavShareServiceModels.Constants;
using WavShareServiceModels.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavShareService.Controllers
{

    public class AudioFilesController : ApiControllerBase
    {
        private IAudioFileBLL _audioFileBLL;
        private ILogger<AudioFilesController> _logger;

        public AudioFilesController(IAudioFileBLL audioFileBLL, ILogger<AudioFilesController> logger)
        {
            _audioFileBLL = audioFileBLL;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve audio files
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(GetAudioFilesResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetAudioFilesRequest>>> Get([FromQuery] GetAudioFilesRequest requestParams)
        {
            var results = await _audioFileBLL.GetAudioFiles(requestParams);
            return Ok(results);
        }

        /// <summary>
        /// Create new audio file
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAudioFileRequest requestBody)
        {
            var newAudioFileId = await _audioFileBLL.CreateAudioFile(requestBody);

            if (newAudioFileId.HasValue)
            {
                return CreatedAtAction(nameof(Get), new { AudioFileId = newAudioFileId }, requestBody);
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
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateAudioFileRequest requestBody)
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
        /// <param name="requestParams"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteAudioFileRequest requestParams)
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
