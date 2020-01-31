using AutoMy.DomainModels;
using AutoMy.ServiceModels;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMyWebsite.Models
{
    [Validator(typeof(AccountValidator))]
    public class AccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }

    public class AccountValidator : AbstractValidator<AccountViewModel>
    {
        public AccountValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.Username).NotEmpty();
            RuleFor(o => o.Password).NotEmpty();
        }
    }
}
