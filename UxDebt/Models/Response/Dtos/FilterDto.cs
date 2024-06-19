using UxDebt.Entities;
using static UxDebt.Enum;

namespace UxDebt.Models.Response.Dtos
{
    public class FilterDto
    {
        public string? Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Discarded { get; set; }
        public Status? Status { get; set; }
        public List<int>? Tags { get; set; }
        public List<int>? Labels { get; set; }
        
    }
}
