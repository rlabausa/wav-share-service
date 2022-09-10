using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavShareServiceModels.AudioFiles
{
    public class DeleteAudioFileRequest
    {
        [Required]
        public int AudioFileId { get; set; }
    }
}
