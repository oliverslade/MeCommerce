using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    using System.Collections.Generic;

    [Table("AspNetRoles")]
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            Users = new HashSet<AspNetUsers>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> Users { get; set; }
    }
}