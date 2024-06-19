using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UxDebt.Entities
{
    public class Repository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RepositoryId { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public int GitId { get; set; }

        [Required]
        public string HtmlUrl { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Issue>? Issues { get; set; }

    }
}
