using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceBLL.Validators;
using WavShareServiceDAL;
using WavShareServiceModels.AudioFiles;
using WavShareServiceModels.Exceptions;

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
            return await _audioFileAdapter.GetAudioFiles(requestParams);
        }

        public async Task<int?> CreateAudioFile(CreateAudioFileRequest requestBody)
        {
            //TODO: Implement BLL validation
            //var validationMessage = AudioFileValidator.Validate(requestBody);

            //if(!string.IsNullOrEmpty(validationMessage))
            //{
            //    throw new ApiException(HttpStatusCode.BadRequest, validationMessage);
            //}

            return await _audioFileAdapter.CreateAudioFile(requestBody);
        }

        public async Task<bool> UpdateAudioFile(UpdateAudioFileRequest requestBody)
        {
            return await _audioFileAdapter.UpdateAudioFile(requestBody);
        }

        public async Task<bool> DeleteAudioFile(DeleteAudioFileRequest requestParams)
        {
            return await _audioFileAdapter.DeleteAudioFile(requestParams);
        }

    }
}
