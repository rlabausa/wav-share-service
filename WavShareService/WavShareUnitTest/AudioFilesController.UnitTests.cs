using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WavShareService.Controllers;
using WavShareServiceBLL;
using WavShareServiceModels.AudioFiles;

namespace WavShareUnitTest
{
    public class AudioFilesControllerTest
    {
        [Fact]
        public async Task Get_Returns_OkObjectResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new GetAudioFilesRequest();

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
                .ReturnsAsync(1);


            // Act
            var result = await audioFileController.Post(request);

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
            var result = await audioFileController.Put(request);

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

            var request = new UpdateAudioFileRequest(
                    1,
                    "hello_there.wav",
                    "ZmFrZV9iYXNlNjRfZW5jb2RlZF9zdHJpbmc=",
                    "General Kenobi"
                );

            mockAudioFileBLL.Setup(x => x.UpdateAudioFile(request))
                .ReturnsAsync(true);


            // Act
            var result = await audioFileController.Put(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}