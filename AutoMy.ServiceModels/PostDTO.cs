using AutoMy.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.ServiceModels
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int PublishingYear { get; set; }
        public FuelType fuelType { get; set; }
        public int VelocityOfEngine { get; set; }
        public int DistanceDrove { get; set; }
        public int Cilinders { get; set; }
        public string GearBox { get; set; }
        public string DriveWheels { get; set; }
        public string Doors { get; set; }
        public WheelType Wheel { get; set; }
        public string Color { get; set; }
        public string CabinColor { get; set; }
        public int AirbagCount { get; set; }
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
        public bool TechBrowsing { get; set; }
        public string Description { get; set; }

    }
}
