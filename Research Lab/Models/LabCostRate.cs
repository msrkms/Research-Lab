using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Research_Lab.Models
{
    public class LabCostRate
    {

        [Key]
        public int id { get; set; }
        public int Rlid { get; set; }

        public float costperminitue { get; set; }


        
        public virtual ResearchLab ResearchLab { get; set; }
    }
}
