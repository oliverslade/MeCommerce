using System.Collections.Generic;

namespace MeCommerceAdmin.Models
{
    public class AspNetRolesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserViewModel> Users { get; set; }
    }
}