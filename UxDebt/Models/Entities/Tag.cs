using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UxDebt.Models.Entities;

namespace UxDebt.Entities
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }        

    }
}
