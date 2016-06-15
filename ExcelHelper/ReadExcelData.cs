using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using AnnualReportCrawler.DAL;
using LinqToExcel;

namespace AnnualReportCrawler.ExcelHelper
{
    public class ReadExcelData
    {
        private ExcelQueryFactory excelQueryFactory;

        private DBMapper dbMapper;

        public ReadExcelData()
        {
            dbMapper = new DBMapper();
        }

        public List<string> ReadExcel(string filePath, string companyName)
        {
            var sheetNames = new List<string>();
            excelQueryFactory = new ExcelQueryFactory(filePath);

            var company = new Company();
            company.Name = companyName;
            company.Id = dbMapper.SaveCompany(company);

            sheetNames = excelQueryFactory.GetWorksheetNames().ToList();
            foreach (var sheet in sheetNames)
            {
                if (sheet.Contains("Segment"))
                {
                    ReadSegmentDetails(company.Id, sheet);
                }
                else if (sheet.Contains("Inco"))
                {
                    ReadIncomeDetails(company.Id, sheet);
                }
            }

            return sheetNames.ToList();
        }

        private void ReadSegmentDetails(int companyId, string sheet)
        {
            var columnNames = excelQueryFactory.GetColumnNames(sheet);
            var tempResult = from data in excelQueryFactory.Worksheet(sheet) select data;
            var resultSet = new List<CompanySegment>();

            foreach (var row in tempResult)
            {
                var data = new CompanySegment();

                for (var index = 0; index < row.Count; index++)
                {
                    switch (index)
                    {
                        case 0:
                            data.RowHeader = row[index];
                            break;
                        case 1:
                            data.ValueCell1 = row[index];
                            break;
                        case 2:
                            data.ValueCell2 = row[index];
                            break;
                        case 3:
                            data.ValueCell3 = row[index];
                            break;
                        case 4:
                            data.ValueCell4 = row[index];
                            break;
                        case 5:
                            data.ValueCell5 = row[index];
                            break;
                    }

                }
                if (string.IsNullOrEmpty(data.RowHeader))
                {
                    data.IsHeaderRow = 1;
                }

                data.SheetName = sheet;
                data.CompanyId = companyId;
                resultSet.Add(data);
            }

            foreach (var item in resultSet)
            {
                dbMapper.SaveCompanySegment(item);
            }
        }

        private void ReadIncomeDetails(int companyId, string sheet)
        {
            var columnNames = excelQueryFactory.GetColumnNames(sheet);
            var tempResult = from data in excelQueryFactory.Worksheet(sheet) select data;
            var resultSet= new List<CompanyIncome>();
            
            foreach (var row in tempResult)
            {
                var data = new CompanyIncome();
                
                for (var index = 0; index < row.Count; index++)
                {
                    switch (index)
                    {
                        case 0:
                            data.RowHeader = row[index];
                            break;
                        case 1:
                            data.ValueCell1 = row[index];
                            break;
                        case 2:
                            data.ValueCell2 = row[index];
                            break;
                        case 3:
                            data.ValueCell3 = row[index];
                            break;
                        case 4:
                            data.ValueCell4 = row[index];
                            break;
                        case 5:
                            data.ValueCell5 = row[index];
                            break;
                    }

                }
                if(string.IsNullOrEmpty(data.RowHeader))
                {
                    data.IsHeaderRow = 1;
                }

                data.SheetName = sheet;
                data.CompanyId = companyId;
                resultSet.Add(data);
            }

            foreach (var item in resultSet)
            {
                dbMapper.SaveCompanyIncome(item);
            }
        }
    }
}
