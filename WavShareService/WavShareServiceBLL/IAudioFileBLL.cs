using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.AudioFiles;

namespace WavShareServiceBLL
{
    public interface IAudioFileBLL
    {
        public Task<GetAudioFilesResponse> GetAudioFiles(GetAudioFilesRequest requestParams);

    }
}
