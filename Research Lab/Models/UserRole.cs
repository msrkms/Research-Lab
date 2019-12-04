using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Research_Lab.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            AppUser = new HashSet<AppUser>();
        }

        [Key]
        public int RoleId { get; set; }
        public string RoleType { get; set; }

        public virtual ICollection<AppUser> AppUser { get; set; }
    }
}
