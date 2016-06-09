using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.Model
{
    public class Report
    {
        public string Name { get; set; }

        public string URL { get; set; }

        public string CIK { get; set; }

        public string Filing { get; set; }

        public DateTime FilingDate { get; set; }

        public string Format { get; set; }

        public string Interactive { get; set; }

        public string FileFilmNumber { get; set; }

        public string ReportPath { get; set; }

    }
}
