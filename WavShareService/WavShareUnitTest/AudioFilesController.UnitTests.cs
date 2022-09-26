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
        public async Task Get_Returns_GetAudioFilesResponse()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AudioFilesController>>();
            var mockAudioFileBLL = new Mock<IAudioFileBLL>();
            var audioFileController = new AudioFilesController(mockAudioFileBLL.Object, mockLogger.Object);

            var request = new GetAudioFilesRequest();


            // Act
            var result = await audioFileController.Get(request);

            // Assert
            Assert.IsType<ActionResult<IEnumerable<GetAudioFilesResponse>>>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_Returns_IActionResult()
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
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(result);
        }
    }
}