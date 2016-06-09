using AnnualReportCrawler.CrawlingLayer;
using AnnualReportCrawler.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AnnualReportCrawler.ViewModel
{
    public class SECReportSearchViewModel : NotifyPropertyChanged
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private string companyName;

        public string CompanyName
        {
            get
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
        private SECReportCrawler secReportCrawler;
        private List<CompanyReport> companies;
        private CompanyReport selectedCompany;
        private List<Report> companyReports;

        public ICommand SearchCompany
        {
            get
            {
                return seearchCompany ?? (
                      seearchCompany = new CommandHandler(
                          () => SearchCompanySECReport(), true
                          ));
            }
        }

        public List<CompanyReport> Companies
        {
            get
            { return companies; }
            set
            {
                companies = value;
                OnPropertyChanged("Companies");
            }
        }

        public CompanyReport SelectedCompany
        {
            get
            { return selectedCompany; }
            set
            {
                selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
                LoadCompanyReport();
            }
        }

        public List<Report> CompanyReports
        {
            get
            { return companyReports; }
            set
            {
                companyReports = value;
                OnPropertyChanged("CompanyReports");

            }
        }

        private Report selectedCompanyReport;
        public Report SelectedCompanyReport
        {
            get
            { return selectedCompanyReport; }
            set
            {
                selectedCompanyReport = value;
                OnPropertyChanged("SelectedCompanyReport");
                LoadReport();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        private bool showCompanyReports;
        public bool ShowCompanyReports
        {
            get { return showCompanyReports; }
            set
            {
                showCompanyReports = value;
                OnPropertyChanged("ShowCompanyReports");
            }
        }

        //

        private async void SearchCompanySECReport()
        {
            IsBusy = true;
            try
            {
                secReportCrawler = new SECReportCrawler(companyName);
                Companies = await secReportCrawler.CrawlCompanyReport();                
                SelectedCompany = Companies.FirstOrDefault();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nPlease try again!");
            }
            IsBusy = false;
        }

        private async void LoadCompanyReport()
        {
            IsBusy = true;
            if (selectedCompany != null)
            {
                try
                {
                    CompanyReports = await secReportCrawler.LoadCompanyReport(selectedCompany);
                    ShowCompanyReports = CompanyReports.Count > 0;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\nPlease try again!");
                }
            }
            IsBusy = false;
        }



        private async void LoadReport()
        {
            if (SelectedCompanyReport != null)
            {
                try
                {
                    IsBusy = true;
                    var report = await secReportCrawler.LoadReport(SelectedCompanyReport);
                    IsBusy = false;
                    if (!string.IsNullOrEmpty(report.ReportPath))
                    {
                        var excel = new Process();
                        excel.StartInfo.FileName = report.ReportPath;
                        excel.Start();

                        // Need to wait for excel to start
                        excel.WaitForInputIdle();
                    }
                }
                catch(InvalidOperationException e)
                {
                    IsBusy = false;                    
                }
                catch (Exception e)
                {
                    IsBusy = false;
                    MessageBox.Show(e.Message + "\nPlease try again!");
                }
            }
        }
    }
}
