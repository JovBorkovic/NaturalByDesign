using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Task
    {
        public Task()
        {
            Labour = new HashSet<Labour>();
        }

        public int ID { get; set; }

        [Display(Name = "Task Description")]
        [Required(ErrorMessage = "You cannot leave task description blank.")]
        [StringLength(80, ErrorMessage = "Description cannot be more than 80 characters long.")]
        public string TaskDescription { get; set; }

        [Display(Name = "Est. End")]
        [Required(ErrorMessage = "Please select an end date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EstCompleteDate { get; set; }

        public virtual ICollection<Labour> Labour { get; set; }
    }
}
