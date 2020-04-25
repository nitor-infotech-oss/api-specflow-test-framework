using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework.Helper
{
    public class TestHelper
    {
        public SqlConnection _sqlConnection = null;

        public bool verifyApiResponseStatusCode(string expectedCode, string jsonResonse)
        {
            if (jsonResonse.Contains(expectedCode))
                return true;
            else
                return false;
        }
        public bool verifyApiResponseStatusCode(int expectedCode, string jsonResonse)
        {
            if (jsonResonse.Contains(expectedCode.ToString()))
                return true;
            else
                return false;
        }

        public bool verifyApiJsonResponse(string expectedResponse, string actualResponse)
        {
            if (expectedResponse.Equals(actualResponse))
                return true;
            else
                return false;
        }

        public object getSqlQueryResult(string query, string connectionString)
        {
            try
            {
                _sqlConnection.Close();
                _sqlConnection.ConnectionString = ConfigurationManager.AppSettings["Sql_ConnectionString"];
                _sqlConnection.Open();
            }
            catch (Exception e)
            {
                _sqlConnection.ConnectionString = ConfigurationManager.AppSettings["Sql_ConnectionString"];
                _sqlConnection.Open();
            }
            var command = new SqlCommand(query, _sqlConnection);
            command.CommandTimeout = 60;
            var sqlReturn = command.ExecuteReader();
            return sqlReturn;

        }



    }
}
