using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    [Table("AspNetUserClaims")]
    public class AspNetUserClaims
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}