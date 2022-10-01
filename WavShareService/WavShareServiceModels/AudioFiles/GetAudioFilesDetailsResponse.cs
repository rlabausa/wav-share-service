using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class GetAudioFilesDetailsResponse
    {
        public GetAudioFilesDetailsResponse(int totalRecords = 0, IEnumerable<AudioFile>? audioFiles = null)
        {
            TotalRecords = totalRecords;
            AudioFilesDetails = audioFiles ?? new List<AudioFile>();

        }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }

        [JsonPropertyName("audioFilesDetails")]
        public IEnumerable<AudioFileDetails> AudioFilesDetails { get; set; }
    }
}
