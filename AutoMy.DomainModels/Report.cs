using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.DomainModels
{
    public class Report
    {
        public int Id { get; set; }
        public string SenderAccountId { get; set; }
        public int PostId { get; set; }
        public string Reason { get; set; }
    }
}
