using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Bids
    {
        public Bids()
        {
            this.BidInventories = new HashSet<BidInventory>();
        }
        public int ID { get; set; }

        [Display(Name = "Estimated Amount")]
        [Required(ErrorMessage = "Please enter an estimated amount.")]
        [DataType(DataType.Currency)]
        public decimal? EstAmount { get; set; }

        [Display(Name = "Actual Amount")]
        [DataType(DataType.Currency)]
        public decimal? ActAmount { get; set; }

        [Display(Name = "Is approved by client?")]
        public bool IsApprovedByClient { get; set; }

        [Display(Name = "Is approved by NBD staff?")]
        public bool IsApprovedByNBD { get; set; }

        [Display(Name = "Revison Check?")]
        public bool RevisionCheck { get; set; }

        [Display(Name = "Project")]
        [Required(ErrorMessage = "Project is required")]
        public int ProjectsID { get; set; }
        public virtual Projects Projects { get; set; }

        [Display(Name = "NBD Staff")]
        [Required(ErrorMessage = "Staff is required")]
        public int? NbdstaffID { get; set; }
        public virtual Nbdstaff Nbdstaff { get; set; }

        [Display(Name = "Occupation/Position")]
        [Required(ErrorMessage = "Occupation is required")]
        public int? OccupationID { get; set; }
        public virtual Occupation Occupation { get; set; }

        [Display(Name = "Sale")]
        [Required(ErrorMessage = "Sale is required")]
        public int? SaleID { get; set; }
        public virtual Sale Sale { get; set; }

        [Display(Name = "Designer")]
        [Required(ErrorMessage = "designer is required")]
        public int? DesignerID { get; set; }
        public virtual Designer Designer { get; set; }

        [Display(Name = "Inventory")]
        [Required(ErrorMessage = "Please select an inventory item")]
        public ICollection<BidInventory> BidInventories { get; set; }
    }

}
