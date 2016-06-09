using AnnualReportCrawler.CrawlingLayer;
using AnnualReportCrawler.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;


namespace AnnualReportCrawler.ViewModel
{
    public class SearchCompanyViewModel: NotifyPropertyChanged
    {
        private CompanyAnnualReportCrawler crawler;
        private string companyName;

        public string CompanyName
        { get
            {
                return companyName;
            }
            set
            {
                companyName = value;
                OnPropertyChanged("CompanyName");
            }
        }

        private ICommand seearchCompany;
        public ICommand SearchCompany
        { get
            { return seearchCompany ?? (
                    seearchCompany = new CommandHandler(
                        () => SearchCompanyAnnualReport(), true
                        ));
            }
        }

        private ICommand downloadReport;
        public ICommand DownloadReport
        {
            get
            {
                return downloadReport ?? (
                      downloadReport = new CommandHandler(
                          () => DownloadAnnualReport(), true
                          ));
            }
        }

        private ICommand crawlReport;
        public ICommand CrawlReport
        {
            get
            {
                return crawlReport ?? (
                      crawlReport = new CommandHandler(
                          () => CrawlReportFile(), true
                          ));
            }
        }

        private List<AnnualReportSearch> reportSearch;

        public List<AnnualReportSearch> ReportSearch
        {
            get
            {
                return reportSearch;
            }
            set
            {
                reportSearch =value;
                OnPropertyChanged("ReportSearch");
            }
        }

        private List<CompanyReport> companyReport;

        public List<CompanyReport> CompanyReport
        {
            get
            {
                return companyReport;
            }
            set
            {
                companyReport = value;
                OnPropertyChanged("CompanyReport");
            }
        }

        private bool isReportSearchVisible;
        public bool IsReportSearchVisile
        {
            get { return isReportSearchVisible; }
            set {
                isReportSearchVisible = value;
                OnPropertyChanged("IsReportSearchVisile");
            }
        }//

        private bool isCompanyReportVisile;
        public bool IsCompanyReportVisile
        {
            get { return isCompanyReportVisile; }
            set
            {
                isCompanyReportVisile = value;
                OnPropertyChanged("IsCompanyReportVisile");
            }
        }


        private AnnualReportSearch selectedCompany;

        public AnnualReportSearch SelectedCompany
        {
            get
            {
                return selectedCompany;
            }
            set
            {
                selectedCompany = value;
                
                OnPropertyChanged("SelectedCompany");
                LoadCompanyReport();
            }
        }//

        private CompanyReport selectedReport;

        public CompanyReport SelectedReport
        {
            get
            {
                return selectedReport;
            }
            set
            {
                selectedReport = value;

                OnPropertyChanged("SelectedReport");
            }
        }

        public async void SearchCompanyAnnualReport()
        {
            crawler = new CompanyAnnualReportCrawler(CompanyName);
            ReportSearch = await crawler.CrawlCompanyAsync();
            
            IsReportSearchVisile = ReportSearch.Count > 1;
            IsCompanyReportVisile = !IsReportSearchVisile;
            if (ReportSearch.Count == 1)
            {
                SelectedCompany = ReportSearch[0];
                LoadCompanyReport();
            }
        }

        private async void LoadCompanyReport()
        {
            IsCompanyReportVisile = true;
            if (crawler == null)
            {
                crawler = new CompanyAnnualReportCrawler(CompanyName);
            }
            if (selectedCompany != null)
            {
                CompanyReport = await crawler.CrawlCompanyReport(selectedCompany.ReportLink);
                if(CompanyReport.Count > 0)
                {
                    SelectedReport = CompanyReport[0];
                }
            }
        }

        private string fileName;
        private bool isPDF;

        private void DownloadAnnualReport()
        {
            if(SelectedReport!=null)
            {
                SaveFileDialog saveDialogue = new SaveFileDialog();
                saveDialogue.RestoreDirectory = true;
                saveDialogue.FileName = SelectedReport.Name;
                if (saveDialogue.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDialogue.FileName;
                    using (WebClient client = new WebClient())
                    {
                        isPDF = false;
                        if (SelectedReport.Name.Contains("HTML"))
                        {
                            
                            client.DownloadFile(SelectedReport.AnnualReportLink, saveDialogue.FileName + ".html");
                        }
                        else if (SelectedReport.Name.Contains("PDF"))
                        {
                            isPDF = true;
                            client.DownloadFile(SelectedReport.AnnualReportLink, saveDialogue.FileName + ".pdf");
                        }
                    }
                }
            }
        }

        private void CrawlReportFile()
        {
            if (fileName != null && isPDF)
            {
                using (var reader = new PdfReader(fileName + ".pdf"))
                {
                    for (int i = 1; i < reader.NumberOfPages; i++)
                    {
                        var text = PdfTextExtractor.GetTextFromPage(reader, i);
                    }
                }
            }
        }
    }
}
