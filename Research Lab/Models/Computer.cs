using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Research_Lab.Models
{
    public partial class Computer
    {
        public Computer()
        {
            BookingInfo = new HashSet<BookingInfo>();
            LabUseCosts = new HashSet<LabUseCost>();
        }

        [Key]
        public int Cid { get; set; }
        public bool IsAvailable { get; set; }
        public int LabId { get; set; }

        public virtual ResearchLab Lab { get; set; }
        public virtual ICollection<BookingInfo> BookingInfo { get; set; }

        public virtual ICollection<LabUseCost> LabUseCosts { get; set; }

    }
}
