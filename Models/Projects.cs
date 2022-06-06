using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Bids = new HashSet<Bids>();
        }
        public int ID { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "You cannot leave project name blank.")]
        [StringLength(50, ErrorMessage = "Project name cannot be more than 50 characters long.")]
        public string PName { get; set; }

        [Display(Name = "Est. Start")]
        [Required(ErrorMessage = "Please select a start date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstStart { get; set; }

        [Display(Name = "Est. End")]
        [Required(ErrorMessage = "Please select an end date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstEnd { get; set; }

        [Display(Name = "Project Site")]
        [Required(ErrorMessage = "A Project Site is required for a Project")]
        //[StringLength(50, ErrorMessage = "Project name cannot be more than 50 characters long.")]
        public string PSite { get; set; }

        [Display(Name = "Actual Start")]
        [Required(ErrorMessage = "Please select a start date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActStartDate { get; set; }

        [Display(Name = "Actual End")]
        [Required(ErrorMessage = "Please select an end date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActEndDate { get; set; }

        [Display(Name = "Bid Amount")]
        [Required(ErrorMessage = "Please enter a bid amount.")]
        [DataType(DataType.Currency)]
        public decimal BidAmount { get; set; }

        [Display(Name = "Client")]
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
    }
}
