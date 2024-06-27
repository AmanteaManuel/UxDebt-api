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
        public string? Labels { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Observation { get; set; }
        public int? RepositoryId { get; set; }
        public string? RepositoryName { get; set; }
        public virtual ICollection<Tag>? Tags { get; set; }
    }
}
