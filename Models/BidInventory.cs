using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.Models
{
    public class BidInventory
    {
        public int InventoryID { get; set; }
        public Inventory Inventory { get; set; }

        public int BidsID { get; set; }
        public Bids Bids { get; set; }
    }
}
