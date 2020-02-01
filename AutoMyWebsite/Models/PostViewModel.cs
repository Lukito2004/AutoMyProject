using AutoMy.DomainModels;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMyWebsite.Models
{
    [Validator(typeof(PostValidator))]
    public class PostViewModel
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public int CategoryId { get; set; }
        public string CityName { get; set; }
        public string VinCode { get; set; }
        public BuyType buyType { get; set; }
        public decimal Price { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int PublishingYear { get; set; }
        public FuelType fuelType { get; set; }
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

        public IEnumerable<SelectListItem> GearBoxListItem { get; set; }
        public IEnumerable<SelectListItem> FuelListItem { get; set; }
        public IEnumerable<SelectListItem> CategoryListItem { get; set; }
        public IEnumerable<SelectListItem> DriveWheelsListItem { get; set; }
    }
    public class PostValidator : AbstractValidator<PostViewModel>
    {
        public PostValidator()
        {
            RuleFor(o => o.ImageUrl1).NotEmpty();
            RuleFor(o => o.ImageUrl2).NotEmpty();
            RuleFor(o => o.ImageUrl3).NotEmpty();
            RuleFor(o => o.Price).GreaterThan(100M).WithMessage("Please check your price");
            RuleFor(o => o.Model).NotEmpty();
            RuleFor(o => o.Company).NotEmpty();
            RuleFor(o => o.PublishingYear).NotEmpty();
            RuleFor(o => o.PublishingYear).GreaterThan(1960);
            RuleFor(o => o.PublishingYear).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(o => o.fuelType).NotEmpty();
            RuleFor(o => o.VelocityOfEngine).NotEmpty();
            RuleFor(o => o.Mileage).NotEmpty();
            RuleFor(o => o.Cilinders).NotEmpty();
            RuleFor(o => o.GearBox).NotEmpty();
            RuleFor(o => o.DriveWheels).NotEmpty();
            RuleFor(o => o.Doors).NotEmpty();
            RuleFor(o => o.Wheel).NotEmpty();
            RuleFor(o => o.Color).NotEmpty();
            RuleFor(o => o.CabinColor).NotEmpty();
            RuleFor(o => o.AirbagCount).NotEmpty();
            RuleFor(o => o.ABS).NotEmpty();
            RuleFor(o => o.ElectronicWindows).NotEmpty();
            RuleFor(o => o.AirConditioner).NotEmpty();
            RuleFor(o => o.ClimateControl).NotEmpty();
            RuleFor(o => o.LeatherSaloon).NotEmpty();
            RuleFor(o => o.Disks).NotEmpty();
            RuleFor(o => o.Navigation).NotEmpty();
            RuleFor(o => o.CentralLocker).NotEmpty();
            RuleFor(o => o.Hatch).NotEmpty();
            RuleFor(o => o.Signaling).NotEmpty();
            RuleFor(o => o.OnBoardComputer).NotEmpty();
            RuleFor(o => o.Hydraulics).NotEmpty();
            RuleFor(o => o.AntiSkid).NotEmpty();
            RuleFor(o => o.ArmchairsHeating).NotEmpty();
            RuleFor(o => o.ParkingControl).NotEmpty();
            RuleFor(o => o.BackViewCamera).NotEmpty();
            RuleFor(o => o.TechInspection).NotEmpty();
            RuleFor(o => o.Description).NotEmpty();
        }
    }
}
