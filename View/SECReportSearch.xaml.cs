using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AnnualReportCrawler.ExcelHelper;

namespace AnnualReportCrawler.View
{
    /// <summary>
    /// Interaction logic for SECReportSearch.xaml
    /// </summary>
    public partial class SECReportSearch : UserControl
    {
        public SECReportSearch()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var readExcelData = new ReadExcelData();
            var sheets = readExcelData.ReadExcel(@"C:\Program Files (x86)\EuromonitorInternational\CompanyAnnualReport\ReportsRepository\0000320187_10-K.xlsx", "Nike");
            
        }
    }
}
