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

        public IEnumerable<SelectListItem> WheelsListItem { get; set; }
        public IEnumerable<SelectListItem> FuelListItem { get; set; }
        public IEnumerable<SelectListItem> CategoryListItem { get; set; }
    }
    public class PostValidator : AbstractValidator<PostViewModel>
    {
        public PostValidator()
        {
            RuleFor(o => o.Name).NotEmpty();
            RuleFor(o => o.ImageUrl).NotEmpty();
            RuleFor(o => o.Price).GreaterThan(100M).WithMessage("Please check your price");
            RuleFor(o => o.Model).NotEmpty();
            RuleFor(o => o.Company).NotEmpty();
            RuleFor(o => o.PublishingYear).NotEmpty();
            RuleFor(o => o.PublishingYear).GreaterThan(1960);
            RuleFor(o => o.PublishingYear).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(o => o.fuelType).NotEmpty();
            RuleFor(o => o.VelocityOfEngine).NotEmpty();
            RuleFor(o => o.DistanceDrove).NotEmpty();
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
            RuleFor(o => o.TechBrowsing).NotEmpty();
            RuleFor(o => o.Description).NotEmpty();
        }
    }
}
