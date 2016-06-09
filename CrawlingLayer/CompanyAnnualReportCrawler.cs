using AnnualReportCrawler.Model;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.CrawlingLayer
{
    public class CompanyAnnualReportCrawler
    {
        private ScrapingBrowser Browser;
        

        private string AnnualReportSearchUrl = "http://www.annualreports.com/Companies?search={0}&submit=Search";

        private string AnnualReportBaseUrl = "http://www.annualreports.com";

        public CompanyAnnualReportCrawler(string companyName)
        {
            Browser = new ScrapingBrowser();
            Browser.AllowAutoRedirect = true;
            Browser.AllowMetaRedirect = true;
            this.CompanyName = companyName;
        }

        private string CompanyName { get; set; }

        public List<AnnualReportSearch> Result { get; set; }

        public async Task<List<AnnualReportSearch>> CrawlCompanyAsync()
        {
            var pageResult = await Browser.NavigateToPageAsync(new Uri(string.Format(AnnualReportSearchUrl, CompanyName)));

            Result = new List<AnnualReportSearch>();
            var listBlock = pageResult.Html.CssSelect(".list-block").FirstOrDefault().Descendants("tbody").FirstOrDefault();
            if (listBlock != null)
            {
                foreach (var node in listBlock.ChildNodes)
                {
                    var aHref = node.SelectNodes(@"td/a");
                    if (aHref != null)
                    {
                        Result.Add(new AnnualReportSearch
                        {
                            ReportLink = aHref.FirstOrDefault().GetAttributeValue("href"),
                            CompanyName = aHref.FirstOrDefault().InnerText
                        });
                    }
                }
            }
            return Result;
        }

        public async Task<List<CompanyReport>> CrawlCompanyReport(string reportUrl)
        {
            var pageResult = await Browser.NavigateToPageAsync(new Uri(AnnualReportBaseUrl + reportUrl));

            var companyReport = new List<CompanyReport>();
            var nodes = pageResult.Html.CssSelect(".links").FirstOrDefault().SelectNodes(@"li/a");

            foreach (var node in nodes)
            {
                if (!(node.InnerText.Contains("Interactive")))
                {
                    companyReport.Add(new CompanyReport
                    {
                        AnnualReportLink = AnnualReportBaseUrl + node.GetAttributeValue("href"),
                        Name = node.InnerText
                    });
                }

            }
            return companyReport;
            

        }

    }
}
