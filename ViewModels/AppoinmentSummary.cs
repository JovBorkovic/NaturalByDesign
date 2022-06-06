using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.ViewModels
{
    public class AppoinmentSummary
    {
        public int ID { get; set; }



        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public double TotalAddedInventoryAmount { get; set; }

        public string PName { get; set; }

    }
}
