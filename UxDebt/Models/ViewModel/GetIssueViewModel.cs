using UxDebt.Entities;
using static UxDebt.Enum;

namespace UxDebt.Models.ViewModel
{
    public class GetIssueViewModel
    {
        
        public int IssueId { get; set; }        
        public long GitId { get; set; }        
        public string HtmlUrl { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }
        public bool Discarded { get; set; }
        public virtual ICollection<Label>? Labels { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Observation { get; set; }
        public Repository? Repository { get; set; }
        public virtual ICollection<Tag>? Tags { get; set; }
    }
}
