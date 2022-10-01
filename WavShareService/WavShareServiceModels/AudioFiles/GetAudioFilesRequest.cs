﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.ApiRequests;

namespace WavShareServiceModels.AudioFiles
{
    public class GetAudioFilesRequest: ApiRequestHeaders
    {
        [FromQuery]
        public int? AudioFileId { get; set; }
        
        [FromQuery]
        [StringLength(100)]
        public string? AudioFileName { get; set; }

        [FromQuery]
        [StringLength(100)]
        public string? UploadedBy { get; set; }

        public GetAudioFilesRequest(string clientCorrelId): base(clientCorrelId)
        {

        }

        public GetAudioFilesRequest(int? audioFileId, string? audioFileName, string? uploadedBy, string? clientCorrelId) : base(clientCorrelId)
        {
            AudioFileId = audioFileId;
            AudioFileName = audioFileName;
            UploadedBy = uploadedBy;
        }
    }
}
