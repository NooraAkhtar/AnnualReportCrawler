using AnnualReportCrawler.ExcelHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnnualReportCrawler.View;

namespace AnnualReportCrawler.DAL
{
    public class DBMapper
    {
        private DBHelper dbHelper;

        public DBMapper()
        {
            dbHelper = new DBHelper();
        }

        public int SaveCompany(Company company)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar) {Value = company.Name});
            parameters.Add(new SqlParameter("@result", SqlDbType.Int) {Direction = ParameterDirection.Output});

            var resultParameter = dbHelper.ExecuteNonQuery("spCompany_Save", parameters.ToArray());

            int returnValue = 0;
            foreach (SqlParameter item in resultParameter)
            {
                if (item.Direction == System.Data.ParameterDirection.Output)
                {
                    returnValue = (int) item.Value;
                    break;
                }
            }
            return returnValue;
        }

        public void SaveCompanyIncome(CompanyIncome companyIncome)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@rowHeader", SqlDbType.NVarChar) {Value = companyIncome.RowHeader});
            parameters.Add(new SqlParameter("@valueCell1", SqlDbType.NVarChar) {Value = companyIncome.ValueCell1 ?? ""});
            parameters.Add(new SqlParameter("@valueCell2", SqlDbType.NVarChar) {Value = companyIncome.ValueCell2 ?? ""});
            parameters.Add(new SqlParameter("@valueCell3", SqlDbType.NVarChar) {Value = companyIncome.ValueCell3 ?? ""});
            parameters.Add(new SqlParameter("@valueCell4", SqlDbType.NVarChar) {Value = companyIncome.ValueCell4 ?? ""});
            parameters.Add(new SqlParameter("@valueCell5", SqlDbType.NVarChar) {Value = companyIncome.ValueCell5 ?? ""});
            parameters.Add(new SqlParameter("@isHeaderRow", SqlDbType.Int) {Value = companyIncome.IsHeaderRow});
            parameters.Add(new SqlParameter("@sheetName", SqlDbType.NVarChar) {Value = companyIncome.SheetName});
            parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) {Value = companyIncome.CompanyId});

            dbHelper.ExecuteNonQuery("spCompanyIncome_Save", parameters.ToArray());

        }

        public void SaveCompanySegment(CompanySegment companySegment)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@rowHeader", SqlDbType.NVarChar) {Value = companySegment.RowHeader});
            parameters.Add(new SqlParameter("@valueCell1", SqlDbType.NVarChar) {Value = companySegment.ValueCell1 ?? ""});
            parameters.Add(new SqlParameter("@valueCell2", SqlDbType.NVarChar) {Value = companySegment.ValueCell2 ?? ""});
            parameters.Add(new SqlParameter("@valueCell3", SqlDbType.NVarChar) {Value = companySegment.ValueCell3 ?? ""});
            parameters.Add(new SqlParameter("@valueCell4", SqlDbType.NVarChar) {Value = companySegment.ValueCell4 ?? ""});
            parameters.Add(new SqlParameter("@valueCell5", SqlDbType.NVarChar) {Value = companySegment.ValueCell5 ?? ""});
            parameters.Add(new SqlParameter("@isHeaderRow", SqlDbType.Int) {Value = companySegment.IsHeaderRow});
            parameters.Add(new SqlParameter("@sheetName", SqlDbType.NVarChar) {Value = companySegment.SheetName});
            parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) {Value = companySegment.CompanyId});

            dbHelper.ExecuteNonQuery("spCompanySegment_Save", parameters.ToArray());

        }

        public List<Company> GetCompanies()
        {
            var dataTable = dbHelper.SelectDataTable("spCompany_Get", null);
            var companies = new List<Company>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var company = new Company();
                company.Id = dataRow["id"] == DBNull.Value ? 1 : Convert.ToInt32(dataRow["id"]);
                company.Name = dataRow["name"] == DBNull.Value ? "" : Convert.ToString(dataRow["name"]);
                companies.Add(company);
            }
            return companies;
        }

        internal CompanyDetail GetCompanies(int id)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = id });

            var dataSet = dbHelper.SelectDataSet("spCompanyDetails_Get", parameters.ToArray());
            var companyDetails = new CompanyDetail();
            var incomeInfo = new List<CompanyIncome>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                var info = new CompanyIncome();
                info.Id = dataRow["id"] == DBNull.Value ? 1 : Convert.ToInt32(dataRow["id"]);
                info.RowHeader = dataRow["RowHeader"] == DBNull.Value ? "" : Convert.ToString(dataRow["RowHeader"]);
                info.ValueCell1 = dataRow["valueCell1"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell1"]);
                info.ValueCell2 = dataRow["valueCell2"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell2"]);
                info.ValueCell3 = dataRow["valueCell3"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell3"]);
                info.ValueCell4 = dataRow["valueCell4"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell4"]);
                info.ValueCell5 = dataRow["valueCell5"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell5"]);
                info.IsHeaderRow = dataRow["isHeaderRow"] == DBNull.Value ? 1 : Convert.ToInt32(dataRow["isHeaderRow"]);
                info.SheetName = dataRow["sheetName"] == DBNull.Value ? "" : Convert.ToString(dataRow["sheetName"]);
                info.CompanyId = dataRow["companyId"] == DBNull.Value ? 0 : Convert.ToInt32(dataRow["companyId"]);
                incomeInfo.Add(info);
            }
            companyDetails.CompanyIncomeInfo = incomeInfo;

            var segmentInfo = new List<CompanySegment>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                var info = new CompanySegment();
                info.Id = dataRow["id"] == DBNull.Value ? 1 : Convert.ToInt32(dataRow["id"]);
                info.RowHeader = dataRow["RowHeader"] == DBNull.Value ? "" : Convert.ToString(dataRow["RowHeader"]);
                info.ValueCell1 = dataRow["valueCell1"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell1"]);
                info.ValueCell2 = dataRow["valueCell2"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell2"]);
                info.ValueCell3 = dataRow["valueCell3"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell3"]);
                info.ValueCell4 = dataRow["valueCell4"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell4"]);
                info.ValueCell5 = dataRow["valueCell5"] == DBNull.Value ? "" : Convert.ToString(dataRow["valueCell5"]);
                info.IsHeaderRow = dataRow["isHeaderRow"] == DBNull.Value ? 1 : Convert.ToInt32(dataRow["isHeaderRow"]);
                info.SheetName = dataRow["sheetName"] == DBNull.Value ? "" : Convert.ToString(dataRow["sheetName"]);
                info.CompanyId = dataRow["companyId"] == DBNull.Value ? 0 : Convert.ToInt32(dataRow["companyId"]);
                segmentInfo.Add(info);
            }
            companyDetails.CompanySegments = segmentInfo;


            return companyDetails;
        } 
    }
}
