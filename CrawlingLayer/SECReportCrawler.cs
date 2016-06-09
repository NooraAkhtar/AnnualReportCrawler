using AnnualReportCrawler.Model;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.CrawlingLayer
{
    public class SECReportCrawler
    {
        private string SECSearchURL = "http://www.sec.gov/cgi-bin/browse-edgar?company={0}&owner=exclude&action=getcompany";
        private string SECBaseURL = "http://www.sec.gov";
        private ScrapingBrowser Browser;
        private string companyName;

        public SECReportCrawler(string companyName)
        {
            this.companyName = companyName;
            Browser = new ScrapingBrowser();
        }

        public List<CompanyReport> Companies{ get; set; }
        public List<Report> Reports { get; private set; }

        internal async Task<List<CompanyReport>> CrawlCompanyReport()
        {
            var pageResult = await Browser.NavigateToPageAsync(new Uri(string.Format(SECSearchURL, companyName)));

            Companies = new List<CompanyReport>();
            var nodes = pageResult.Html.CssSelect(".tableFile2").FirstOrDefault().SelectNodes("tr");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var company = new CompanyReport();
                    var childNodes = node.ChildNodes;

                    if (childNodes[1].SelectNodes("a") != null)
                    {

                        var cikNode = childNodes[1].SelectNodes("a").FirstOrDefault();
                        company.CIK = cikNode.InnerText;
                        company.AnnualReportLink = cikNode.GetAttributeValue("href").Replace("&amp;", "&");
                        var companyName = childNodes[3];
                        company.Name = companyName.InnerText.Replace("&amp;", "&");

                        var state = childNodes[5];
                        company.State = state.InnerText.Replace("&amp;", "&");
                        Companies.Add(company);
                    }
                }
            }
            return Companies;            
        }

        internal async Task<List<Report>> LoadCompanyReport(CompanyReport selectedCompany)
        {
            var pageResult = await Browser.NavigateToPageAsync(new Uri(SECBaseURL + selectedCompany.AnnualReportLink));

            Reports = new List<Report>();
            var nodes = pageResult.Html.CssSelect(".tableFile2").FirstOrDefault().SelectNodes("tr");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var report = new Report();
                    var childNodes = node.ChildNodes;

                    if (childNodes != null)
                    {
                        if (childNodes[1].Name != "th"
                            && (childNodes[3].ChildNodes != null && childNodes[3].ChildNodes.Count == 3))
                        {
                            var cikNode = childNodes[1];
                            report.Filing = cikNode.InnerText;

                            var format = childNodes[3];
                            report.Format = format.InnerText.Replace("&nbsp;", "");
                            if (format.ChildNodes != null && format.ChildNodes.Count == 3) {
                                report.Format = format.ChildNodes[0].InnerText.Replace("&nbsp;", "");
                                report.Interactive = format.ChildNodes[2].GetAttributeValue("href").Replace("&amp;", "&");
                            }

                            report.URL = format.GetAttributeValue("href").Replace("&amp;", "&");

                            var name = childNodes[5];
                            report.Name = name.InnerText.Replace("&nbsp;", "");

                            DateTime date = DateTime.Now;
                            if (DateTime.TryParse(childNodes[7].InnerText, out date))
                            {
                                report.FilingDate = date;
                            }

                            report.CIK = selectedCompany.CIK;

                            Reports.Add(report);
                        }
                    }
                }
            }
            return Reports;
        }

        internal async Task<Report> LoadReport(Report selectedCompanyReport)
        {
            selectedCompanyReport.ReportPath = System.Environment.CurrentDirectory
                + "\\ReportsRepository\\"
                + selectedCompanyReport.CIK + "_"
                + selectedCompanyReport.Filing;            
            try
            {
                if (!Directory.Exists(System.Environment.CurrentDirectory + "\\ReportsRepository"))
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\ReportsRepository");
                }

                if (!File.Exists(selectedCompanyReport.ReportPath))
                {

                    var url = selectedCompanyReport.Interactive;

                    var pageResult = await Browser.NavigateToPageAsync(new Uri(SECBaseURL + selectedCompanyReport.Interactive));

                    var nodes = pageResult.Html.CssSelect(".xbrlviewer");
                    foreach (var node in nodes)
                    {
                        if (node.InnerText == "View Excel Document")
                        {
                            var reportURL = node.GetAttributeValue("href");

                            using (WebClient client = new WebClient())
                            {

                                selectedCompanyReport.ReportPath = selectedCompanyReport.ReportPath
                                    + Path.GetExtension(SECBaseURL + reportURL);
                                client.DownloadFile(SECBaseURL + reportURL,
                                    selectedCompanyReport.ReportPath);
                            }
                            break;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                selectedCompanyReport.ReportPath = null;
            }
            
            return selectedCompanyReport;
        }
    }
}
