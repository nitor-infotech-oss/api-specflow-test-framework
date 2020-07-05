using ApiFramework.APIs.SimpleGET;
using ApiFramework.APIs.SimpleGetAPI;
using ApiFramework.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework.APIs.BaiscAuthenticationAPI
{
    public class ValidateTheResponse
    {
        TestHelper testHelper = new TestHelper();
        public WebClientHelper clientHelper = new WebClientHelper();
        public JsonHelper jsonHelper = new JsonHelper();

        SqlDataReader sqlReturn;

        public void verifyJsonResponseWithDatabase(string jsonResponse, SimpleGetInputClass inputParameters)
        {
            var jsonResponseClass = JsonConvert.DeserializeObject<OutputClass>(jsonResponse);
            string email = null, first_name = null, last_name = null;

            string query = "select * from " + jsonHelper.GetDataByEnvironment("Profile_Table")
                           +" where id = " + inputParameters.id;

            sqlReturn = testHelper.getSqlQueryResult(query);

            while (sqlReturn.Read())
            {
                email = sqlReturn["email"].ToString();
                first_name = sqlReturn["first_name"].ToString();
                last_name = sqlReturn["last_name"].ToString();
            }

            if (email != jsonResponseClass.data.email)
            {
                Hooks.test.Fail("Email value mismatch with database.");
            }
            else
            {
                Hooks.test.Pass("Title value match with database.");
            }

            if (first_name != jsonResponseClass.data.first_name)
            {
                Hooks.test.Fail("first_name value mismatch with database.");
            }
            else
            {
                Hooks.test.Pass("first_name value match with database.");
            }

            if (last_name != jsonResponseClass.data.last_name)
            {
                Hooks.test.Fail("last_name value mismatch with database.");
            }
            else
            {
                Hooks.test.Pass("last_name value match with database.");
            }

        }
    }
}
