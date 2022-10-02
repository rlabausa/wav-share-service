using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WavShareServiceModels.AudioFiles;

namespace WavShareServiceDAL
{
    public class AudioFileAdapter : IAudioFileAdapter
    {

        private string _dbConnString;
        private IConfiguration _configuration;
        public AudioFileAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnString = configuration.GetConnectionString("WavShareDB");
        }

        private async Task<int> GetTotalAudioFileCount(GetAudioFilesRequest requestParams)

        {
            using (var connection = new SqlConnection(_dbConnString))
            {
                await connection.OpenAsync();
                int totalCount = 0;

                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[GetTotalAudioFileCount]";

                    if (requestParams != null)
                    {
                        if (requestParams.AudioFileId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@file_id", requestParams.AudioFileId.Value);
                        }

                        if (!string.IsNullOrEmpty(requestParams.AudioFileName))
                        {
                            cmd.Parameters.AddWithValue("@file_name", requestParams.AudioFileName);
                        }

                        if (!string.IsNullOrEmpty(requestParams.UploadedBy))
                        {
                            cmd.Parameters.AddWithValue("@file_uploaded_by", requestParams.UploadedBy);
                        }
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            totalCount = await reader.GetFieldValueAsync<int>(0);
                        }
                    }
                }

                return totalCount;
            }
        }

        public async Task<GetAudioFilesDetailsResponse> GetAudioFilesDetails(GetAudioFilesRequest requestParams)
        {
            var audioFilesDetails = new List<AudioFileDetails>();
            var totalRecords = await this.GetTotalAudioFileCount(requestParams);

            if (totalRecords > 0)
            {
                using (var connection = new SqlConnection(_dbConnString))
                {
                    await connection.OpenAsync();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[GetAudioFiles]";

                        if (requestParams != null)
                        {
                            if (requestParams.AudioFileId.HasValue)
                            {
                                cmd.Parameters.AddWithValue("@file_id", requestParams.AudioFileId.Value);
                            }

                            if (!string.IsNullOrEmpty(requestParams.AudioFileName))
                            {
                                cmd.Parameters.AddWithValue("@file_name", requestParams.AudioFileName);
                            }

                            if (!string.IsNullOrEmpty(requestParams.UploadedBy))
                            {
                                cmd.Parameters.AddWithValue("@file_uploaded_by", requestParams.UploadedBy);
                            }

                            cmd.Parameters.AddWithValue("@page_cursor", requestParams.PageCursor);

                            cmd.Parameters.AddWithValue("@page_limit", requestParams.PageLimit);
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                AudioFileDetails detailRecord = new AudioFileDetails();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var colName = reader.GetName(i);
                                    Object colValue = await reader.GetFieldValueAsync<object>(i);

                                    var prop = detailRecord.GetType().GetProperty(colName);

                                    if (prop != null)
                                    {
                                        prop.SetValue(detailRecord, colValue);
                                    }
                                   
                                }

                                if (!string.IsNullOrEmpty(detailRecord.AudioFileName))
                                {
                                    audioFilesDetails.Add(detailRecord);
                                }

                            }
                        }


                    }

                }
            }

            return new GetAudioFilesDetailsResponse()
            {
                TotalRecords = totalRecords,
                AudioFilesDetails = audioFilesDetails
            };
        }
        
        public async Task<AudioFile> GetAudioFileById(int id)
        {
            AudioFile? audioFile = null;

            var results = await GetAudioFiles(new GetAudioFilesRequest() { AudioFileId = id });

            if (results != null && results.AudioFiles != null && results.AudioFiles.Any())
            {
                audioFile = results.AudioFiles.First();
            }

            return audioFile;
        }
        
        public async Task<GetAudioFilesResponse> GetAudioFiles(GetAudioFilesRequest requestParams)
        {
            var audioFiles = new List<AudioFile>();
            var totalRecords = await this.GetTotalAudioFileCount(requestParams);

            if (totalRecords > 0)
            {
                using (var connection = new SqlConnection(_dbConnString))
                {
                    await connection.OpenAsync();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[GetAudioFiles]";

                        if (requestParams != null)
                        {
                            if (requestParams.AudioFileId.HasValue)
                            {
                                cmd.Parameters.AddWithValue("@file_id", requestParams.AudioFileId.Value);
                            }

                            if (!string.IsNullOrEmpty(requestParams.AudioFileName))
                            {
                                cmd.Parameters.AddWithValue("@file_name", requestParams.AudioFileName);
                            }

                            if (!string.IsNullOrEmpty(requestParams.UploadedBy))
                            {
                                cmd.Parameters.AddWithValue("@file_uploaded_by", requestParams.UploadedBy);
                            }
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                AudioFile audioFile = new AudioFile();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var colName = reader.GetName(i);
                                    Object colValue = await reader.GetFieldValueAsync<object>(i);

                                    switch (colName)
                                    {
                                        case "AudioFileId":
                                            audioFile.AudioFileId = (int)colValue;
                                            break;
                                        case "AudioFileName":
                                            audioFile.AudioFileName = (string)colValue;
                                            break;
                                        case "EncodedAudio":
                                            audioFile.EncodedAudio = (string)colValue;
                                            break;
                                        case "UploadedBy":
                                            audioFile.UploadedBy = (string)colValue;
                                            break;
                                        case "UploadDate":
                                            audioFile.UploadDate = (DateTime)colValue;
                                            break;
                                    }
                                }

                                if (!string.IsNullOrEmpty(audioFile.AudioFileName))
                                {
                                    audioFiles.Add(audioFile);
                                }

                            }
                        }


                    }

                }
            }

            return new GetAudioFilesResponse()
            {
                TotalRecords = totalRecords,
                AudioFiles = audioFiles
            };
        }

        public async Task<AudioFileDetails> CreateAudioFile(CreateAudioFileRequest requestBody)
        {
            AudioFileDetails newAudioFileDetails = new AudioFileDetails();

            using (var connection = new SqlConnection(_dbConnString))
            {
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[CreateAudioFile]";

                    cmd.Parameters.AddWithValue("@audio_file_name", requestBody.AudioFileName);
                    cmd.Parameters.AddWithValue("@encoded_audio", requestBody.EncodedAudio);
                    cmd.Parameters.AddWithValue("@uploaded_by", requestBody.UploadedBy);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var colName = reader.GetName(i);
                                Object colValue = await reader.GetFieldValueAsync<object>(i);

                                var prop = newAudioFileDetails.GetType().GetProperty(colName);

                                if (prop != null)
                                {
                                    prop.SetValue(newAudioFileDetails, colValue);
                                }
                            }
                        }

                    }
                }

                return newAudioFileDetails;
            }
        }

        public async Task<bool> UpdateAudioFile(UpdateAudioFileRequest requestBody)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(_dbConnString))
            {
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[UpdateAudioFile]";

                    cmd.Parameters.AddWithValue("@audio_file_id", requestBody.AudioFileId);
                    cmd.Parameters.AddWithValue("@audio_file_name", requestBody.AudioFileName);
                    cmd.Parameters.AddWithValue("@encoded_audio", requestBody.EncodedAudio);
                    cmd.Parameters.AddWithValue("@uploaded_by", requestBody.UploadedBy);

                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }

            bool success = rowsAffected > 0;

            return success;
        }

        public async Task<bool> DeleteAudioFile(DeleteAudioFileRequest requestParams)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(_dbConnString))
            {
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[DeleteAudioFile]";

                    cmd.Parameters.AddWithValue("@audio_file_id", requestParams.AudioFileId);

                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }

            bool success = rowsAffected >= 0;

            return success;
        }

    }
}
