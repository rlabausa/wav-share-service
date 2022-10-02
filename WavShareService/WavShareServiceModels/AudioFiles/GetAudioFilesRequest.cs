using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WavShareServiceModels.ApiRequests;
using WavShareServiceModels.Constants;

namespace WavShareServiceModels.AudioFiles
{
    public class GetAudioFilesRequest
    {
        public const int MIN_PAGE_LIMIT = 1;
        public const int MAX_PAGE_LIMIT = 1000;
        public const int DEFAULT_PAGE_LIMIT = 10;
        public const int DEFAULT_PAGE_CURSOR = 0;

        [FromQuery]
        public int? AudioFileId { get; set; }

        [FromQuery]
        [StringLength(100)]
        public string? AudioFileName { get; set; }

        [FromQuery]
        [StringLength(100)]
        public string? UploadedBy { get; set; }

        [FromQuery]
        [DefaultValue(DEFAULT_PAGE_CURSOR)]
        [Range(DEFAULT_PAGE_CURSOR, int.MaxValue, ErrorMessage = $"The value for {nameof(PageCursor)} must be greater than 0.")]
        public int PageCursor { get; set; }

        [FromQuery]
        [Range(MIN_PAGE_LIMIT, MAX_PAGE_LIMIT)]
        [DefaultValue(DEFAULT_PAGE_LIMIT)]
        public int PageLimit { get; set; }

        public GetAudioFilesRequest()
        {
            PageCursor = DEFAULT_PAGE_CURSOR;
            PageLimit = DEFAULT_PAGE_LIMIT;
        }

        public GetAudioFilesRequest(int? audioFileId, string? audioFileName, string? uploadedBy, int? pageCursor, int? pageLimit)
        {
            AudioFileId = audioFileId;
            AudioFileName = audioFileName;
            UploadedBy = uploadedBy;
            PageCursor = pageCursor ?? DEFAULT_PAGE_CURSOR;
            PageLimit = pageLimit.HasValue ? Math.Min(pageLimit.Value, MAX_PAGE_LIMIT) : DEFAULT_PAGE_LIMIT;
        }
    }
}
