using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.Models
{
    public class Designer
    {
        public int ID { get; set; }

        [Display(Name = "Designer")]
        [Required(ErrorMessage = "You cannot leave designer blank.")]
        public string Designers { get; set; }

        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Nbdstaff> Nbdstaffs { get; set; }
    }
}
