using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entities
{
    public class Repository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RepositoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }

    }
}
