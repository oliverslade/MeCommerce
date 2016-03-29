namespace DomainModels
{
    using System.Collections.Generic;

    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            this.Users = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> Users { get; set; }
    }
}