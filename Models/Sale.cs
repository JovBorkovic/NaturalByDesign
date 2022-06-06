using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.Models
{
    public class Sale
    {
        public int ID { get; set; }

        [Display(Name = "Sales")]
        [Required(ErrorMessage = "You cannot leave sales blank.")]
        public string Sales { get; set; }

        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Nbdstaff> Nbdstaffs { get; set; }
    }
}
