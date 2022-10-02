using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.Constants;

namespace WavShareServiceModels.AudioFiles
{
    public class DeleteAudioFileRequest
    {
        [Required]
        [FromQuery]
        [Range(0, int.MaxValue, ErrorMessage = "The value for AudioFileId must be greater than 0.")]
        public int? AudioFileId { get; set; }

        public DeleteAudioFileRequest()
        {

        }

        public DeleteAudioFileRequest(int? audioFileId)
        {
            AudioFileId = audioFileId;
        }
    }
}
