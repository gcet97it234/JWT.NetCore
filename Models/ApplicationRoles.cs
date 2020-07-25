using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.NetCore.Models
{
    public class ApplicationRoles : IdentityRole<Guid>
    {

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual UserInformation User { get; set; }
        public virtual ApplicationRoles Role { get; set; }
    }
}
