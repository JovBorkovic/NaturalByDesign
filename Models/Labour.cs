using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Labour
    {
        public int ID { get; set; }

        [Display(Name = "Hours")]
        //[Required(ErrorMessage = "Please select an end date.")]
        //[DataType(DataType.Time)]
        public int Hours { get; set; }

        [Display(Name = "Labour Description")]
        [Required(ErrorMessage = "You cannot leave Labour Description blank.")]
        [StringLength(80, ErrorMessage = "Item Size cannot be more than 80 characters long.")]
        public string LabourDesc { get; set; }
        //public decimal? PricePerHour { get; set; }
        //public decimal? ExtPrice { get; set; }
        //public int FkTask { get; set; }
        //public int FkStaff { get; set; }

        //public virtual Nbdstaff FkStaffNavigation { get; set; }
        //public virtual Task FkTaskNavigation { get; set; }

        [Display(Name = "NBD Staff")]
        public int NbdstaffID { get; set; }
        public virtual Nbdstaff Nbdstaff { get; set; }

        [Display(Name = "Bid")]
        public int BidsID { get; set; }
        public virtual Bids Bids { get; set; }

        [Display(Name = "Task")]
        public int TaskID { get; set; }
        public virtual Task Task { get; set; }

        [Display(Name = "Occupation/Position")]
        public int? OccupationID { get; set; }
        public virtual Occupation Occupation { get; set; }
    }
}
