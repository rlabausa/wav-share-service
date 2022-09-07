using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceDAL;
using WavShareServiceModels.AudioFiles;

namespace WavShareServiceBLL
{
    public class AudioFileBLL: IAudioFileBLL
    {
        private IAudioFileAdapter _audioFileAdapter;
        public AudioFileBLL(IAudioFileAdapter audioFileAdapter)
        {
            _audioFileAdapter = audioFileAdapter; 
        }

        public async Task<GetAudioFilesResponse> GetAudioFiles(GetAudioFilesRequest requestParams)
        {
            if(requestParams != null)
            {

            }

            return await _audioFileAdapter.GetAudioFiles(requestParams);
        }

    }
}
