using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.ExcelHelper
{
    public class CompanyIncome
    {
        public int Id { get; set; }

        public string RowHeader { get; set; }

        public string ValueCell1 { get; set; }
        public string ValueCell2 { get; set; }
        public string ValueCell3 { get; set; }
        public string ValueCell4 { get; set; }
        public string ValueCell5 { get; set; }

        public int IsHeaderRow { get; set; }

        public string SheetName { get; set; }

        public int CompanyId { get; set; }
    }
}
