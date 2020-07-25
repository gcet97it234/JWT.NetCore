
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Utilities.Models
{
    public class UserInformation : IdentityUser<Guid>
    {
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual CompanyInfo Company { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
