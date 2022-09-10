using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.AudioFiles;

namespace WavShareServiceBLL.Validators
{
    public static class AudioFileValidator
    {
        public static bool IsValid(GetAudioFilesRequest queryParams)
        {
            if (queryParams == null)
            {
                return true;
            }
            else
            {
                if (queryParams.AudioFileId.HasValue)
                {

                }

                if (!string.IsNullOrEmpty(queryParams.AudioFileName))
                {

                }
            }
            return false;
        }
        public static bool IsValid(CreateAudioFileRequest requestBody)
        {
            return true;
        }
        public static string Validate(CreateAudioFileRequest requestBody)
        {

            if (requestBody == null)
            {
                return "Request body cannot be empty.";
            }

            if (string.IsNullOrEmpty(requestBody.AudioFileName))
            {
                return "Parameter audioFileName cannot be empty.";
            }

            if (string.IsNullOrEmpty(requestBody.UploadedBy))
            {
                return "Parameter uploadedBy cannot be empty.";
            }
            
            if (string.IsNullOrEmpty(requestBody.EncodedAudio))
            {
                return "Parameter encodedAudio cannot be empty.";
            }

            if (!StringHasValidBase64Encoding(requestBody.EncodedAudio))
            {
                return "Parameter encodedAudio must be a Base64 encoded string.";
            }

            return string.Empty;
        }
        private static bool StringHasValidBase64Encoding(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            try
            {
                var buffer = Convert.FromBase64String(str);

                if (buffer == null || buffer.Length <= 0)
                {
                    return false;
                }

            }
            catch (ArgumentNullException exc)
            {
                Console.Error.WriteLine(exc.Message, exc.StackTrace);
                return false;
            }
            catch (FormatException exc)
            {
                Console.Error.WriteLine(exc.Message, exc.StackTrace);
                return false;
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message, exc.StackTrace);
                return false;
            }

            return true;

        }
    }
}
