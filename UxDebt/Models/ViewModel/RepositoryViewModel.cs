using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UxDebt.Entities;

namespace UxDebt.Models.ViewModel
{
    public class RepositoryViewModel
    {
        public int RepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public int GitId { get; set; }

        [Required]
        public string HtmlUrl { get; set; }

        public string? Description { get; set; }
       
    }
}
