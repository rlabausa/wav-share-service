using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class AudioFile
    {
        public AudioFile()
        {
            AudioFileId = -1;
            AudioFileName = string.Empty;
            EncodedAudio = string.Empty;
            UploadedBy = string.Empty;
            UploadDate = DateTime.UnixEpoch;
        }

        public AudioFile(int audioFileId, string audioFileName, string encodedAudio, string uploadedBy, DateTime uploadDate)
        {
            AudioFileId = audioFileId;
            AudioFileName = audioFileName;
            EncodedAudio = encodedAudio;
            UploadedBy = uploadedBy;
            UploadDate = uploadDate;
        }

        [JsonPropertyName("audioFileId")]
        public int AudioFileId { get; set; }

        [JsonPropertyName("audioFileName")]
        public string AudioFileName { get; set; }

        [JsonPropertyName("encodedAudio")]
        public string EncodedAudio { get; set; }

        [JsonPropertyName("uploadedBy")]
        public string UploadedBy { get; set; }

        [JsonPropertyName("uploadDate")]
        public DateTime UploadDate { get; set; }
    }
}
