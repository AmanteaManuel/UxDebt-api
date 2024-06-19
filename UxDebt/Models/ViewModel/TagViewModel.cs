using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UxDebt.Models.Entities;

namespace UxDebt.Models.ViewModel
{
    public class TagViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }            

    }
}
