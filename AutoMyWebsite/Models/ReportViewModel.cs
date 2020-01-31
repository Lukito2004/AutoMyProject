using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMyWebsite.Models
{
    [Validator(typeof(ReportValidator))]
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string SenderAccountId { get; set; }
        public string Reason { get; set; }
        public int PostId { get; set; }
    }

    public class ReportValidator : AbstractValidator<ReportViewModel>
    {
        public ReportValidator()
        {
            RuleFor(o => o.Reason).NotEmpty();
            RuleFor(o => o.SenderAccountId).NotEmpty();
            RuleFor(o => o.PostId).NotEmpty();
        }
    }

}
