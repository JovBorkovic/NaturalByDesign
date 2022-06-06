using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Occupation
    {
        //public Occupation()
        //{
        //    Nbdstaff = new HashSet<Nbdstaff>();
        //}

        public int ID { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "You cannot leave position blank.")]
        [StringLength(50, ErrorMessage = "Position cannot be more than 50 characters long.")]
        public string Position { get; set; }

        [Display(Name = "Position Description")]
        [Required(ErrorMessage = "You cannot leave position description blank.")]
        [StringLength(50, ErrorMessage = "Position description cannot be more than 50 characters long.")]
        public string Description { get; set; }
        
        [Display(Name = "Price Per Hour")]
        [Required(ErrorMessage = "Enter hourly amount")]
        [DataType(DataType.Currency)]
        public decimal PricePerHour { get; set; }

        public virtual ICollection<Nbdstaff> Nbdstaffs { get; set; }
        public virtual ICollection<Labour> Labours { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
    }
}
