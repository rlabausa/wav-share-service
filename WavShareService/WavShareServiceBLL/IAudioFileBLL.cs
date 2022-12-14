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
        public Task<AudioFile> GetAudioFileById(int id);
        public Task<GetAudioFilesDetailsResponse> GetAudioFilesDetails(GetAudioFilesRequest requestParams);
        public Task<GetAudioFilesResponse> GetAudioFiles(GetAudioFilesRequest requestParams);
        public Task<AudioFileDetails> CreateAudioFile(CreateAudioFileRequest requestBody);
        public Task<bool> UpdateAudioFile(UpdateAudioFileRequest requestBody);
        public Task<bool> DeleteAudioFile(DeleteAudioFileRequest requestParams);

    }
}
