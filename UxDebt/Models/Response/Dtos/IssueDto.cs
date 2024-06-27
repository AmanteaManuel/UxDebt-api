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
        public DateTime? ClosedAt { get; set; }

        [JsonProperty("labels")]
        public List<Label>? Labels { get;set; }
        public string? Observation { get; set; }

        /// <summary>
        /// se utiliza para obtener la lista de labels de manera de string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Labels == null || Labels.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(", ", Labels.Select(label => label.Name));
        }

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
