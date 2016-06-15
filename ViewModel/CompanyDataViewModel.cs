using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnnualReportCrawler.ExcelHelper;

namespace AnnualReportCrawler.ViewModel
{
    public class CompanyDataViewModel : NotifyPropertyChanged
    {
        private ReadExcelData readExcelData;


        private List<Company> companies;

        public List<Company> Companies
        {
            get { return companies; }
            set
            {
                companies = value;
                OnPropertyChanged("Companies");
            }
        }

        private Company selectedCompany;

        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
            }
        }

        private ICommand getCompanyDetails;

        public ICommand GetCompanyDetails
        {
            get
            {

                return getCompanyDetails ?? (
                      getCompanyDetails = new CommandHandler(
                          () => GetCompanyDetailedReport(), true
                          ));
            }
        }

        private List<CompanyIncome> companyIncomeInfo;

        public List<CompanyIncome> CompanyIncomeInfo
        {
            get { return companyIncomeInfo; }
            set
            {
                companyIncomeInfo = value;
                OnPropertyChanged("CompanyIncomeInfo");
            }
        }

        private List<CompanySegment> companySegmentInfo;

        public List<CompanySegment> CompanySegmentInfo
        {
            get { return companySegmentInfo; }
            set
            {
                companySegmentInfo = value;
                OnPropertyChanged("CompanySegmentInfo");
            }
        }

        private bool info1Visibility;

        public bool Info1Visibility
        {
            get { return info1Visibility; }
            set
            {
                info1Visibility = value;
                OnPropertyChanged("Cell1Visibility");
            }
        }

        private bool info2Visibility;

        public bool Info2Visibility
        {
            get { return info2Visibility; }
            set
            {
                info2Visibility = value;
                OnPropertyChanged("Info2Visibility");
            }
        }

        private bool info3Visibility;

        public bool Info3Visibility
        {
            get { return info3Visibility; }
            set
            {
                info3Visibility = value;
                OnPropertyChanged("Info3Visibility");
            }
        }

        private bool info4Visibility;

        public bool Info4Visibility
        {
            get { return info4Visibility; }
            set
            {
                info4Visibility = value;
                OnPropertyChanged("Info4Visibility");
            }
        }

        private bool info5Visibility;

        public bool Info5Visibility
        {
            get { return info5Visibility; }
            set
            {
                info5Visibility = value;
                OnPropertyChanged("Info5Visibility");
            }
        }


        private bool cell1Visibility;

        public bool Cell1Visibility
        {
            get { return cell1Visibility; }
            set
            {
                cell1Visibility = value;
                OnPropertyChanged("Cell1Visibility");
            }
        }

        private bool cell2Visibility;

        public bool Cell2Visibility
        {
            get { return cell2Visibility; }
            set
            {
                cell2Visibility = value;
                OnPropertyChanged("Cell2Visibility");
            }
        }

        private bool cell3Visibility;

        public bool Cell3Visibility
        {
            get { return cell3Visibility; }
            set
            {
                cell3Visibility = value;
                OnPropertyChanged("Cell3Visibility");
            }
        }

        private bool cell4Visibility;

        public bool Cell4Visibility
        {
            get { return cell4Visibility; }
            set
            {
                cell4Visibility = value;
                OnPropertyChanged("Cell4Visibility");
            }
        }

        private bool cell5Visibility;

        public bool Cell5Visibility
        {
            get { return cell5Visibility; }
            set
            {
                cell5Visibility = value;
                OnPropertyChanged("Cell5Visibility");
            }
        }

        public CompanyDataViewModel()
        {
            readExcelData = new ReadExcelData();
            Companies = readExcelData.GetAllCompanies();
        }

        public void GetCompanyDetailedReport()
        {
            if (SelectedCompany != null)
            {
                var companyDetail = readExcelData.GetCompanyDetails(SelectedCompany.Id);
                CompanyIncomeInfo = companyDetail.CompanyIncomeInfo;
                CompanySegmentInfo = companyDetail.CompanySegments;

                Info1Visibility = CompanyIncomeInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell1)) > 0;
                Info2Visibility = CompanyIncomeInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell2)) > 0;
                Info3Visibility = CompanyIncomeInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell3)) > 0;
                Info4Visibility = CompanyIncomeInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell4)) > 0;
                Info5Visibility = CompanyIncomeInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell5)) > 0;

                Cell1Visibility = CompanySegmentInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell1))>0;
                Cell2Visibility = CompanySegmentInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell2)) > 0;
                Cell3Visibility = CompanySegmentInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell3)) > 0;
                Cell4Visibility = CompanySegmentInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell4)) > 0;
                Cell5Visibility = CompanySegmentInfo.Count(x => !string.IsNullOrEmpty(x.ValueCell5)) > 0;

            }
        }
    }
}
