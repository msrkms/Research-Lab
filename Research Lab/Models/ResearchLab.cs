using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Research_Lab.Models
{
    public partial class ResearchLab
    {
        public ResearchLab()
        {
            Computer = new HashSet<Computer>();
        }

        [Key]
        public int Rlid { get; set; }
        public string LabName { get; set; }
        public string LabLoction { get; set; }
        public int LabAssistant { get; set; }

        public virtual AppUser LabAssistantNavigation { get; set; }
        public virtual LabUseCost LabUseCost { get; set; }
        public virtual ICollection<Computer> Computer { get; set; }
    }
}
