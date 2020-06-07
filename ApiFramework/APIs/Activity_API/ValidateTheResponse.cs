using ApiFramework.APIs.SimpleGET;
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

        public bool verifyJsonResponseWithDatabase(string jsonResponse)
        {
            var jsonResponseClass = JsonConvert.DeserializeObject<OutputClass>(jsonResponse);
            string dbTitle = null;

            string query = "select * from " + jsonHelper.GetDataByEnvironment("Profile_Table")
                           +" where id = " + jsonResponseClass.ID;

            sqlReturn = testHelper.getSqlQueryResult(query);
            while (sqlReturn.Read())
            {
                dbTitle = sqlReturn["Title"].ToString();
            }

            if(dbTitle != jsonResponseClass.Title)
            {
                Hooks.test.Fail("Title value mismatch with database.");
                return false;
            }
            else
            {
                Hooks.test.Pass("Title value match with database.");
                return true;
            }
        }
    }
}
