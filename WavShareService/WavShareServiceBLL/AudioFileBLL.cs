using System.Net;
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

        public async Task<GetAudioFilesDetailsResponse> GetAudioFilesDetails(GetAudioFilesRequest requestParams)
        {
            return await _audioFileAdapter.GetAudioFilesDetails(requestParams);
        }

        public async Task<AudioFile> GetAudioFileById(int id)
        {
            return await _audioFileAdapter.GetAudioFileById(id);
        }


        public async Task<AudioFileDetails> CreateAudioFile(CreateAudioFileRequest requestBody)
        {
            var validationMessage = AudioFileValidator.Validate(requestBody);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new ApiException(HttpStatusCode.BadRequest, validationMessage);
            }

            return await _audioFileAdapter.CreateAudioFile(requestBody);
        }

        public async Task<bool> UpdateAudioFile(UpdateAudioFileRequest requestBody)
        {
            var validationMessage = AudioFileValidator.Validate(requestBody);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new ApiException(HttpStatusCode.BadRequest, validationMessage);
            }

            return await _audioFileAdapter.UpdateAudioFile(requestBody);
        }

        public async Task<bool> DeleteAudioFile(DeleteAudioFileRequest requestParams)
        {
            return await _audioFileAdapter.DeleteAudioFile(requestParams);
        }

    }
}
