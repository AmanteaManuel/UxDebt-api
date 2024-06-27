using System.ComponentModel.DataAnnotations;
using static UxDebt.Enum;

namespace UxDebt.Models.ViewModel
{
    public class IssueViewModel
    {

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

        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Observation { get; set; }
        public string? Labels { get; set; }

    }
}
