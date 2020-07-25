using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Utilities.Models
{
    public class CompanyInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyKey { get; set; }
        public string LocationKey { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        
        public virtual UserInformation User { get; set; }
    }
}
