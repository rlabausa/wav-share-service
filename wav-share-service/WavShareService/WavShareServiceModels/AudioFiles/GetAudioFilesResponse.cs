using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class GetAudioFilesResponse
    {
        public GetAudioFilesResponse(int totalRecords = 0, IEnumerable<AudioFile>? audioFiles = null)
        {
            TotalRecords = totalRecords;
            AudioFiles = audioFiles ?? new List<AudioFile>();

        }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }

        [JsonPropertyName("audioFiles")]
        public IEnumerable<AudioFile> AudioFiles { get; set; }
    }
}
