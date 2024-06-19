using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UxDebt.Models.Entities;
using static UxDebt.Enum;

namespace UxDebt.Entities
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueId { get; set; }

        [Required]        
        public long GitId { get; set; }

        [Required]
        public string HtmlUrl { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public bool Discarded { get; set; }

        [JsonIgnore]
        public virtual ICollection<Label>? Labels { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Observation { get; set; }        

        public int RepositoryId { get; set; }

        [ForeignKey("RepositoryId")]
        [JsonIgnore]
        public Repository? Repository { get; set; }

        public virtual ICollection<IssueTag>? IssueTags { get; set; }
    }
}
