using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UxDebt.Entities
{
    public class Label
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Issue>? Labels { get; set; }
    }
}
