using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WavShareService.Controllers;
using WavShareServiceBLL;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.AudioFiles;

namespace WavShareUnitTest
{
    public class AudioFilesControllerTest
    {
        private static ApiRequestHeaders REQUEST_HEADERS = new ApiRequestHeaders(Guid.NewGuid().ToString());

        [Fact]
        public async Task Get_Returns_OkObjectResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new GetAudioFilesRequest(REQUEST_HEADERS.ClientCorrelId.ToString());

            // Act
            var result = await audioFileController.Get(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Post_Returns_CreatedAtActionResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new CreateAudioFileRequest(
                    "hello_there.wav",
                    "ZmFrZV9iYXNlNjRfZW5jb2RlZF9zdHJpbmc=",
                    "General Kenobi"
                );

            mockAudioFileBLL.Setup(x => x.CreateAudioFile(request))
                .ReturnsAsync(new AudioFileDetails());


            // Act
            var result = await audioFileController.Post(REQUEST_HEADERS, request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Put_Returns_NoContentResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new UpdateAudioFileRequest(
                    1,
                    "hello_there.wav",
                    "ZmFrZV9iYXNlNjRfZW5jb2RlZF9zdHJpbmc=",
                    "General Kenobi"
                );

            mockAudioFileBLL.Setup(x => x.UpdateAudioFile(request))
                .ReturnsAsync(true);


            // Act
            var result = await audioFileController.Put(REQUEST_HEADERS, request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_Returns_NoContentResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new DeleteAudioFileRequest(REQUEST_HEADERS.ClientCorrelId.ToString(), 1);

            mockAudioFileBLL.Setup(x => x.DeleteAudioFile(request))
                .ReturnsAsync(true);


            // Act
            var result = await audioFileController.Delete(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}