using Newtonsoft.Json;
using static UxDebt.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UxDebt.Entities;
using Microsoft.Data.SqlClient.DataClassification;

namespace UxDebt.Dtos
{
    public class IssueDto
    {
        [JsonProperty("id")]
        public long GitId { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }        

        [JsonProperty("title")]
        public string Title { get; set; }
        

        [JsonProperty("state")]
        public Status Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreateAt { get; set; }

        [JsonProperty("closed_at")]
        public DateTime? CloseAt { get; set; }

        [JsonProperty("labels")]
        public List<Label>? Labels { get;set; }
        public string? Observation { get; set; }

        public class Label
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }

    
}
