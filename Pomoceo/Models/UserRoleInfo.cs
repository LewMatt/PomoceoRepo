using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pomoceo.Models
{
    [NotMapped]
    public class UserRoleInfo
    {
        [NotMapped]
        public ApplicationUser User { set; get; }
        [NotMapped]
        public List<string> Roles { set; get; }
    }
}