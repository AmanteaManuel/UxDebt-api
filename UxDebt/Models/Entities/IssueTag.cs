using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UxDebt.Entities;

namespace UxDebt.Models.Entities
{
    public class IssueTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueTagId { get; set; }        

        public int IssueId { get; set; }
        [ForeignKey("IssueId")]
        public Issue Issue { get; set; }         

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
