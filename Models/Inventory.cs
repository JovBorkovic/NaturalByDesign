using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbdAplication.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            BidInventories = new HashSet<BidInventory>();
            //List = 10d;
        }

        

        public int ID { get; set; }

        [Display(Name = "Inventory Code")]
        [Required(ErrorMessage = "You cannot leave Inventory Code blank.")]
        [StringLength(10, ErrorMessage = "Inventory Code cannot be more than 10 characters long.")]
        public string Code { get; set; }

        [Display(Name = "Item Description")]
        [Required(ErrorMessage = "You cannot leave Item Description blank.")]
        [StringLength(70, ErrorMessage = "Item Description cannot be more than 70 characters long.")]
        public string ItemDescription { get; set; }

        [Display(Name = "Amount(List$)")]
        [Required(ErrorMessage = "Please enter an amount(List$).")]
        [DataType(DataType.Currency)]
        public decimal List { get; set; }

        [Display(Name = "Item Size")]
        [Required(ErrorMessage = "You cannot leave Item Size blank.")]
        [StringLength(10, ErrorMessage = "Item Size cannot be more than 10 characters long.")]
        public string Size { get; set; }

        [Display(Name = "Inventory")]
        public ICollection<BidInventory> BidInventories { get; set; }

        //public int? BidsID { get; set; }
        //public virtual Bids Bids { get; set; }

        [Display(Name = "Material")]
        public int? MaterialID { get; set; }
        public virtual Material Material { get; set; }
    }
}
