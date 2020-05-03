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
        JsonHelper jsonHelper = new JsonHelper();

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

        public SqlDataReader getSqlQueryResult(string query)
        {
            string connetionString;
            SqlConnection cnnection;
            connetionString = jsonHelper.GetDataByEnvironment("SQL_ConnectionString");
            cnnection = new SqlConnection(connetionString);
            cnnection.Open();            

            var command = new SqlCommand(query, cnnection);
            command.CommandTimeout = 60;
            var sqlReturn = command.ExecuteReader();

            return sqlReturn;
        }



    }
}
