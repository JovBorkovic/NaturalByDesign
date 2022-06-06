using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Nbdstaff 
    {
        //public Nbdstaff()
        //{
        //    Labour = new HashSet<Labour>();
        //}

        public int ID { get; set; }

        [Display(Name = "Staff")]
        public string FullName
        {
            get
            {
                return StaffFirst + " " + StaffLast;
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string StaffFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string StaffLast { get; set; }

        [Display(Name = "User name")]
        [Required(ErrorMessage = "You cannot leave user name blank.")]
        [StringLength(50, ErrorMessage = "User name cannot be more than 50 characters long.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You cannot leave password blank.")]
        [StringLength(50, ErrorMessage = "Password cannot be more than 50 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public int Phone { get; set; }
        //public int FkOccupation { get; set; }
        //public virtual Occupation FkOccupationNavigation { get; set; }
        //public virtual ICollection<Labour> Labour { get; set; }
        public virtual ICollection<Labour> Labours { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }

        [Display(Name = "Occupation/Position")]
        public int OccupationID { get; set; }
        public virtual Occupation Occupation { get; set; }

        [Display(Name = "Sale")]
        public int SaleID { get; set; }
        public virtual Sale Sale { get; set; }

        [Display(Name = "Designer")]
        public int DesignerID { get; set; }
        public virtual Designer Designer { get; set; }
    }
}
