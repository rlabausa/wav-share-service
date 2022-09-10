using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class CreateAudioFileRequest
    {
        public string AudioFileName { get; set; }
        public string EncodedAudio { get; set; }
        public string UploadedBy { get; set; }
    }
}
