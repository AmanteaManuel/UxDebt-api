using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UxDebt.Models.Response.Dtos
{
    public class RepositoryDto
    {

        [JsonProperty("id")]
        public int GitId { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Owner Owner { get; set; }    
        
    }

    public class Owner
    {
        public string Login { get; set; }
    }

}
