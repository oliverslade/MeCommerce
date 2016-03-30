using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels
{
    using System.Collections.Generic;

    public class AspNetRoles
    {
        public AspNetRoles()
        {
            Users = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> Users { get; set; }
    }
}