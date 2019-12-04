using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Research_Lab.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            LabUseCost = new HashSet<LabUseCost>();
            BookingInfo = new HashSet<BookingInfo>();
            ResearchLab = new HashSet<ResearchLab>();
        }

        [Key]
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<BookingInfo> BookingInfo { get; set; }

        public virtual ICollection<LabUseCost> LabUseCost { get; set; }
        public virtual ICollection<ResearchLab> ResearchLab { get; set; }
    }
}
