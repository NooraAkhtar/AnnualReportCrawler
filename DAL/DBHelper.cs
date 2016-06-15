using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.DAL
{
    public class DBHelper
    {
        private string connectionString = @"Data Source=INDBAND043\PRODUCTS;Initial Catalog=CompanyReport;UID=sa;Password=admin@123";
        private SqlDataAdapter dataAdapter;
        private SqlCommand command;
        private SqlConnection connection;

        public DBHelper()
        {
            connection = new SqlConnection(connectionString);
        }


        public SqlParameterCollection ExecuteNonQuery(string storeProdure, SqlParameter[] parameters)
        {
            int result = 0;
            SqlParameterCollection resultParamters = null;
            try
            {
                using (SqlCommand command = new SqlCommand(storeProdure, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                    resultParamters = command.Parameters;

                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return resultParamters;
        }

        public DataTable SelectDataTable(string storeProdure, SqlParameter[] parameters)
        {
            var dataTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(storeProdure, connection))
                {
                    if (parameters != null)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);
                }
            }
            catch (SqlException e)
            { }
            finally
            {

            }
            return dataTable;
        }

        public DataSet SelectDataSet(string storeProdure, SqlParameter[] parameters)
        {
            var dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(storeProdure, connection))
                {
                    if (parameters != null)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataSet);
                }
            }
            catch (SqlException e)
            { }
            finally
            {

            }
            return dataSet;
        }

    }
}
