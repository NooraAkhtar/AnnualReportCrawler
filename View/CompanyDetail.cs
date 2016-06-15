using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnnualReportCrawler.ExcelHelper;

namespace AnnualReportCrawler.View
{
    public class CompanyDetail
    {
        public int CompanyId { get; set; }
        public List<CompanyIncome> CompanyIncomeInfo { get; set; }
        public List<CompanySegment> CompanySegments { get; set; }
    }
}
