using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Research_Lab.Models
{
    public partial class BookingInfo
    {
        [Key]
        public int Biid { get; set; }
        public int Cid { get; set; }
        public int AppUserId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime BookingStartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime BookingEndTime { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Computer C { get; set; }
    }
}
