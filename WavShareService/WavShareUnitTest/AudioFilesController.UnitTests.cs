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
        public async Task AudioFilesController_Returns_GetAudioFilesResponse()
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
    }
}