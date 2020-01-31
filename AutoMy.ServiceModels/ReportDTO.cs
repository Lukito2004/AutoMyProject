using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.ServiceModels
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string SenderAccountId { get; set; }
        public string Reason { get; set; }
    }
}
