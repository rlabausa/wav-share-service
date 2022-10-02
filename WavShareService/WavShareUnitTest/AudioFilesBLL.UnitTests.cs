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
using WavShareServiceModels.Exceptions;
using Xunit;

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
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

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

            var result = await audioFileDAL.GetAudioFiles(mockRequest);
            
            Assert.NotNull(result);
            Assert.IsType<GetAudioFilesResponse>(result);

        }
        
        [Fact]
        public async Task GetAudioFileById_Returns_AudioFile()
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = 1;
            var mockResponse = new AudioFile(1, "hello.wav", "aGVsbG8gd29ybGQ=", "ruby", DateTime.Now);

            mockAudioFileAdapter
                .Setup(x => x.GetAudioFileById(mockRequest))
                .ReturnsAsync(mockResponse);

            var result = await audioFileDAL.GetAudioFileById(mockRequest);
            
            Assert.NotNull(result);
            Assert.IsType<AudioFile>(result);

        }

        [Fact]
        public async Task GetAudioFilesDetails_Returns_GetAudioFilesDetailsResponse()
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new GetAudioFilesRequest();
            var mockResponse = new GetAudioFilesDetailsResponse()
            {
                TotalRecords = 1,
                AudioFilesDetails = new List<AudioFileDetails>()
                {
                    new AudioFileDetails (1, "hello.wav", "ruby", DateTime.Now),
                    new AudioFileDetails(2, "goodbye.wav", "john", DateTime.Now)
                }
            };

            mockAudioFileAdapter.Setup(x => x.GetAudioFilesDetails(mockRequest))
                .ReturnsAsync(mockResponse);

            var result = await audioFileDAL.GetAudioFilesDetails(mockRequest);

            Assert.NotNull(result);
            Assert.IsType<GetAudioFilesDetailsResponse>(result);
        }

        [Fact]
        public async Task CreateAudioFile_Returns_AudioFileDetails()
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new CreateAudioFileRequest("happypath.wav", "aGVsbG8gd29ybGQ=", "ruby");
            var mockResponse = new AudioFileDetails(777, mockRequest.AudioFileName, mockRequest.UploadedBy, DateTime.UtcNow);

            mockAudioFileAdapter.Setup(x => x.CreateAudioFile(mockRequest))
                .ReturnsAsync(mockResponse);

            var result = await audioFileDAL.CreateAudioFile(mockRequest);

            Assert.NotNull(result);
            Assert.IsType<AudioFileDetails>(result);
        }
        
        [Fact]
        public async Task Delete_AudioFile_Returns_Bool()
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new DeleteAudioFileRequest(777);
            var mockResponse = true;

            mockAudioFileAdapter.Setup(x => x.DeleteAudioFile(mockRequest))
                .ReturnsAsync(mockResponse);

            var result = await audioFileDAL.DeleteAudioFile(mockRequest);

            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData("invalidString")]
        [InlineData("")]
        [InlineData("nonbase64encodedstring")]
        public async Task CreateAudioFile_Rejects_Non_Base64_Encoded_Strings(string encodedAudio)
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new CreateAudioFileRequest("unhappypath.wav", encodedAudio, "ruby");
            var mockResponse = new AudioFileDetails(777, mockRequest.AudioFileName, mockRequest.UploadedBy, DateTime.UtcNow);

            mockAudioFileAdapter.Setup(x => x.CreateAudioFile(mockRequest))
                .ReturnsAsync(mockResponse);

            await Assert.ThrowsAsync<ApiException>(async () => await audioFileDAL.CreateAudioFile(mockRequest));
        }

        [Theory]
        [InlineData("invalidString")]
        [InlineData("")]
        [InlineData("nonbase64encodedstring")]
        public async Task UpdateAudioFile_Rejects_Non_Base64_Encoded_Strings(string encodedAudio)
        {
            var mockLogger = new Mock<ILogger<AudioFileBLL>>();
            var mockAudioFileAdapter = new Mock<IAudioFileAdapter>();
            var audioFileDAL =  new AudioFileBLL(mockAudioFileAdapter.Object);

            var mockRequest = new UpdateAudioFileRequest(777, "unhappypath.wav", encodedAudio, "ruby");

            mockAudioFileAdapter.Setup(x => x.UpdateAudioFile(mockRequest))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ApiException>(async () => await audioFileDAL.UpdateAudioFile(mockRequest));
        }


    }
}
