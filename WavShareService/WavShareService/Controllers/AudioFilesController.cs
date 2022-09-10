using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WavShareServiceBLL;
using WavShareServiceModels.AudioFiles;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavShareService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioFilesController : ControllerBase
    {
        private IAudioFileBLL _audioFileBLL;
        public AudioFilesController(IAudioFileBLL audioFileBLL)
        {
            _audioFileBLL = audioFileBLL;   
        }

        // GET: api/audiofiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AudioFile>>> Get([FromQuery] GetAudioFilesRequest requestParams)
        {
            var results = await _audioFileBLL.GetAudioFiles(requestParams);
            return Ok(results);
        }       

        // POST api/audiofiles
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAudioFileRequest requestBody)
        {
            var newAudioFileId = await _audioFileBLL.CreateAudioFile(requestBody);

            if (newAudioFileId.HasValue)
            {
                return CreatedAtAction(nameof(Get), new { AudioFileId = newAudioFileId }, requestBody);
            } else
            {
                //TODO: Customize error response
                return BadRequest();
            }
        }

        // PUT api/audiofiles
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/audiofiles
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
