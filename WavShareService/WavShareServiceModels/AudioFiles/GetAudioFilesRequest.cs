using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.ApiRequests;

namespace WavShareServiceModels.AudioFiles
{
    public class GetAudioFilesRequest: ApiRequest
    {
        public int? AudioFileId { get; set; }

        [StringLength(100)]
        public string? AudioFileName { get; set; }

        [StringLength(100)]
        public string? UploadedBy { get; set; }

    }
}
