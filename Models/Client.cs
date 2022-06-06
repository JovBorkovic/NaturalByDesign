using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Client
    {
        public Client()
        {
            //Bids = new HashSet<Bids>();
            Projects = new HashSet<Projects>();
        }

        public int ClientId { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "You cannot leave client name blank.")]
        [StringLength(50, ErrorMessage = "Client name cannot be more than 50 characters long.")]
        public string ClientName { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string ContactFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string ContactLast { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "You cannot leave position blank.")]
        [StringLength(50, ErrorMessage = "Position cannot be more than 50 characters long.")]
        public string ContactPos { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You cannot leave the address blank.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string ContactEmail { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public string ContactPhone { get; set; }

        //public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
