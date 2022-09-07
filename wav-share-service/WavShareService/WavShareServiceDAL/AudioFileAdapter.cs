﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
                    cmd.CommandText = "GetTotalAudioFileCount";

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
                        cmd.CommandText = "GetAudioFiles";

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

    }
}
