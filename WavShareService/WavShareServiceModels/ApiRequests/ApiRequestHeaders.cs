using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WavShareServiceModels.Constants;
using WavShareServiceModels.ValidationAttributes;

namespace WavShareServiceModels.ApiRequests
{
    public class ApiRequestHeaders
    {
        [CorrelId]
        [FromHeader(Name = Header.ClientCorrelId)]
        [JsonPropertyName(Header.ClientCorrelId)]
        [Display(Order = 0)]
        [DefaultValue(ClientCorrelIdHeaderValue.Generate)]
        [Required]
        public string ClientCorrelId { get; set; }

        public ApiRequestHeaders()
        {

        }

        public ApiRequestHeaders(string clientCorrelId)
        {
            ClientCorrelId = clientCorrelId;
        }
    }
}
