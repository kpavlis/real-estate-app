using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class RealEstate
    {
        public int realEstateID { get; }
        public int submitterID { get; }
        public int seller_lessorID { get; private set; }
        public int price { get; private set; }
        public int size { get; private set; }
        public int floor { get; private set; }
        public int year { get; private set; }
        public int bedrooms { get; private set; }
        public bool availability { get; private set; }
        public bool leaseSell { get; private set; }
        public string area { get; private set; }
        public string type { get; private set; }
        public string details { get; private set; }
        public string image { get; private set; }

        public RealEstate(int realEstateID, int submitterID, int seller_lessorID, int price, int size, int floor, int year, int bedrooms, bool availability, bool leaseSell, string area, string type, string details, string image) 
        {
            this.realEstateID = realEstateID;
            this.submitterID = submitterID;
            this.seller_lessorID = seller_lessorID;
            this.price = price;
            this.size = size;
            this.floor = floor;
            this.year = year;
            this.bedrooms = bedrooms;
            this.availability = availability;
            this.leaseSell = leaseSell;
            this.area = area;
            this.type = type;
            this.details = details;
            this.image = image;
        }

        protected void ChangeRealEstateAttributes(int newPrice, int newSize, int newFloor, int newYear, int newBedrooms, bool newAvailability,bool newLeaseSell, string newArea, string newType, string newDetails, string newImage)
        {
            this.price = newPrice;
            this.size = newSize;
            this.floor = newFloor;
            this.year = newYear;
            this.bedrooms = newBedrooms;
            this.availability = newAvailability;
            this.leaseSell = newLeaseSell;
            this.area = newArea;
            this.type = newType;
            this.details = newDetails;
            this.image = newImage;
        }
    }
}
