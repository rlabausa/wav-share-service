using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class AudioFileDetails
    {
        public AudioFileDetails()
        {
            AudioFileId = -1;
            AudioFileName = string.Empty;
            UploadedBy = string.Empty;
            UploadDate = DateTime.UnixEpoch;
        }

        public AudioFileDetails(int audioFileId, string audioFileName, string uploadedBy, DateTime uploadDate)
        {
            AudioFileId = audioFileId;
            AudioFileName = audioFileName;
            UploadedBy = uploadedBy;
            UploadDate = uploadDate;
        }
        [JsonPropertyName("audioFileId")]
        public int AudioFileId { get; set; }

        [JsonPropertyName("audioFileName")]
        public string AudioFileName { get; set; }

        [JsonPropertyName("uploadedBy")]
        public string UploadedBy { get; set; }

        [JsonPropertyName("uploadDate")]
        public DateTime UploadDate { get; set; }
    }
}
