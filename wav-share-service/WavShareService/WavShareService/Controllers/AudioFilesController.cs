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

        // GET: api/<AudioFiles>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AudioFile>>> Get([FromQuery] GetAudioFilesRequest requestParams)
        {
            var results = await _audioFileBLL.GetAudioFiles(requestParams);
            return Ok(results);
        }

        // GET api/<AudioFiles>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AudioFiles>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AudioFiles>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AudioFiles>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
