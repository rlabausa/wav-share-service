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
        public static string MISSING_REQUEST_BODY = "Request body cannot be empty";
        public static string MISSING_AUDIO_FILE_NAME = "Parameter audioFileName cannot be empty.";
        public static string MISSING_UPLOADER = "Parameter uploadedBy cannot be empty.";
        public static string MISSING_ENCODED_AUDIO = "Parameter encodedAudio cannot be empty.";
        public static string INVALID_BINARY_ENCODING = "Parameter encodedAudio must be a Base64 encoded string.";
        
        
        public static string Validate(GetAudioFilesRequest queryParams)
        {
            if (queryParams == null)
            {
                return string.Empty;
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
            return string.Empty;
        }

        /// <summary>
        /// Perform necessary validations for <see cref="CreateAudioFileRequest"/>.
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Returns <see cref="string.Empty"/> if request is valid or a non-empty <see cref="string"/> containing details of the validation failure.</returns>
        public static string Validate(CreateAudioFileRequest requestBody)
        {

            if (requestBody == null)
            {
                return MISSING_REQUEST_BODY;
            }

            if (string.IsNullOrEmpty(requestBody.AudioFileName))
            {
                return MISSING_AUDIO_FILE_NAME;
            }

            if (string.IsNullOrEmpty(requestBody.UploadedBy))
            {
                return MISSING_UPLOADER;
            }
            
            if (string.IsNullOrEmpty(requestBody.EncodedAudio))
            {
                return MISSING_ENCODED_AUDIO;
            }

            if (!StringHasValidBase64Encoding(requestBody.EncodedAudio))
            {
                return INVALID_BINARY_ENCODING;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform necessary validations for <see cref="UpdateAudioFileRequest"/>.
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Returns <see cref="string.Empty"/> if request is valid or a non-empty <see cref="string"/> containing details of the validation failure.</returns>
        public static string Validate(UpdateAudioFileRequest requestBody)
        {

            if (requestBody == null)
            {
                return MISSING_REQUEST_BODY;
            }

            if (string.IsNullOrEmpty(requestBody.AudioFileName))
            {
                return MISSING_AUDIO_FILE_NAME;
            }

            if (string.IsNullOrEmpty(requestBody.UploadedBy))
            {
                return MISSING_UPLOADER;
            }

            if (string.IsNullOrEmpty(requestBody.EncodedAudio))
            {
                return MISSING_ENCODED_AUDIO;
            }

            if (!StringHasValidBase64Encoding(requestBody.EncodedAudio))
            {
                return INVALID_BINARY_ENCODING;
            }

            return string.Empty;
        }


        /// <summary>
        /// Check if a string is a Base64 encoded string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns><see cref="bool"/> that indicates if the string is a Base64 encoded string.</returns>
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
