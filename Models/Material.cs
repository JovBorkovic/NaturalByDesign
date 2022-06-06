using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.Models
{
    public class Material
    {
        public int ID { get; set; }

        [Display(Name = "Material Name")]
        [Required(ErrorMessage = "You cannot leave material name blank.")]
        [StringLength(50, ErrorMessage = "Material name cannot be more than 50 characters long.")]
        public string MaterialName { get; set; }


        //public int BidsID { get; set; }
        //public virtual Bids Bids { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

    }
}
