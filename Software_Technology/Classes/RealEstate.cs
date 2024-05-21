using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class RealEstate
    {
        private int _realEstateID { get; set; }
        private int _submitterID { get; set; }
        private int _seller_leasorID { get; set; }
        private int _price { get; set; }
        private int _size { get; set; }
        private int _floor { get; set; }
        private int _year { get; set; }
        private int _bedrooms { get; set; }
        private bool _availability { get; set; }
        private bool _leaseSell { get; set; }
        private string _area { get; set; }
        private string _type { get; set; }
        private string _details { get; set; }

        public RealEstate() { }

        protected void ChangeRealEstateAttributes()
        {

        }
    }
}
