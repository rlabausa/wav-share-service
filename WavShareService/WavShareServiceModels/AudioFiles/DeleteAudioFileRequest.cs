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
    public class DeleteAudioFileRequest: ApiRequestHeaders
    {
        [Required]
        [FromQuery]
        public int AudioFileId { get; set; }

        public DeleteAudioFileRequest(): base()
        {
            AudioFileId = -1;
        }

        public DeleteAudioFileRequest(string clientCorrelId, int audioFileId): base(clientCorrelId)
        {
            AudioFileId = audioFileId;
        }
    }
}
