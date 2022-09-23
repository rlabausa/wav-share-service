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

namespace WavShareServiceModels.ApiRequests
{
    public class ApiRequest
    {

        [Required]
        [FromHeader(Name = Header.ClientCorrelId)]
        [JsonPropertyName(Header.ClientCorrelId)]
        [Display(Order = 0)]
        [DefaultValue("generate")]
        public string? ClientCorrelId { get; set; }


    }
}
