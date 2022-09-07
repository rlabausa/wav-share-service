namespace wav_share_service.Models
{
    public class AudioFile
    {
        public int AudioFileId { get; set; }
        public string AudioFileName { get; set; }   
        public string EncodedAudio { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
