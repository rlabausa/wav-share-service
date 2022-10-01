using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.ApiRequests;

namespace WavShareServiceModels.AudioFiles
{
    public class CreateAudioFileRequest
    {
        [Required]
        public string AudioFileName { get; set; }

        [Required]
        public string EncodedAudio { get; set; }
        [Required]
        public string UploadedBy { get; set; }

        public CreateAudioFileRequest(string audioFileName, string encodedAudio, string uploadedBy)
        {
            AudioFileName = audioFileName;
            EncodedAudio = encodedAudio;
            UploadedBy = uploadedBy;

        }
    }
}
