using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineParser
{
    public class Royalty_Report
    {
        
        public string? reportPath;
        public string? reportDate;
        public string? reportName;
        public decimal? unitPrice;

        public Royalty_Report(string _reportPath, string _reportDate, string _reportName, decimal? _unitPrice)
        {
           reportPath = _reportPath;
           reportDate = _reportDate;
           reportName = _reportName;
            unitPrice = _unitPrice;
        }
    }
}
