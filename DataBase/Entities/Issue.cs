using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entities
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueId { get; set; }
        public string Name { get; set; }
        public int RepositoryId { get; set; }

        [ForeignKey("RepositoryId")]
        public Repository Repository { get; set; }

    }
}
