using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class AudioFile : AudioFileDetails
    {
        public AudioFile() : base()
        {
            EncodedAudio = string.Empty;
        }

        public AudioFile(int audioFileId, string audioFileName, string encodedAudio, string uploadedBy, DateTime uploadDate)
            : base(audioFileId, audioFileName, uploadedBy, uploadDate)
        {
            EncodedAudio = encodedAudio;
        }

        [JsonPropertyName("encodedAudio")]
        public string EncodedAudio { get; set; }
    }
}
