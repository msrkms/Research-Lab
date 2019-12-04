using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Research_Lab.Models
{
    public class LabUseCost
    {
        [Key]
        public int id { get; set; }


        [DataType(DataType.Date)]
        public DateTime UseDate { get; set; }

        public int hour { get; set; }

        public int minute { get; set; }

        public double totalCost { get; set; }


        public int appUserID { get; set; }
       
        public virtual  AppUser appUser { get; set; }

        public int CId { get; set; }
        public virtual Computer Computer { get; set; }
    }
}
