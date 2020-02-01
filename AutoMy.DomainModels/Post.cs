using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.DomainModels
{
    public class Post
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public int CategoryId { get; set; }
        public string CityName { get; set; }
        public string VinCode { get; set; }
        public decimal Price { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int PublishingYear { get; set; }
        public FuelType fuelType { get; set; }
        public BuyType buyType { get; set; }
        public int VelocityOfEngine { get; set; }
        public int Mileage { get; set; }
        public int Cilinders { get; set; }
        public GearBoxType GearBox { get; set; }
        public LeadingWheels DriveWheels { get; set; }
        public string Doors { get; set; }
        public WheelType Wheel { get; set; }
        public string Color { get; set; }
        public string CabinColor { get; set; }
        public int AirbagCount { get; set; }
        public DateTime PostedTime { get; set; }
        public bool Levied { get; set; }
        public bool Turbo { get; set; }
        public bool ABS { get; set; }
        public bool ElectronicWindows { get; set; }
        public bool AirConditioner { get; set; }
        public bool ClimateControl { get; set; }
        public bool LeatherSaloon { get; set; }
        public bool Disks { get; set; }
        public bool Navigation { get; set; }
        public bool CentralLocker { get; set; }
        public bool Hatch { get; set; }
        public bool Signaling { get; set; }
        public bool OnBoardComputer { get; set; }
        public bool Hydraulics { get; set; }
        public bool AntiSkid { get; set; }
        public bool ArmchairsHeating { get; set; }
        public bool ParkingControl { get; set; }
        public bool BackViewCamera { get; set; }
        public string Description { get; set; }
        public bool TechInspection { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
    }
}
