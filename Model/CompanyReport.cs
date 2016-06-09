using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.Model
{
    public class CompanyReport
    {
        public string Name { get; set; }

        public string AnnualReportLink { get; set; }

        public string State { get; set; }

        public string CIK { get; set; }
    }
}
