using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareService.Controllers;
using WavShareServiceBLL;
using WavShareServiceDAL;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.AudioFiles;

namespace WavShareUnitTest
{
    
    public class AudioFilesBLLTest
    {
        private static ApiRequestHeaders REQUEST_HEADERS = new ApiRequestHeaders(Guid.NewGuid().ToString());

        [Fact]
        public async Task GetAudioFiles_Returns_GetAudioFilesResponse()
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileBLL = new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new GetAudioFilesRequest();
            var mockResponse = new GetAudioFilesResponse()
            {
                AudioFiles = new List<AudioFile>()
                {
                    new AudioFile(1, "hello.wav", "aGVsbG8gd29ybGQ=", "ruby", DateTime.Now),
                    new AudioFile(2, "goodbye.wav", "Z29vZGJ5ZSB3b3JsZA==", "john", DateTime.Now)
                }
            };

            mockAudioFileAdapter
                .Setup(x => x.GetAudioFiles(mockRequest))
                .ReturnsAsync(mockResponse);

            var result = await audioFileBLL.GetAudioFiles(mockRequest);
            
            Assert.NotNull(result);

        }
    }
}
