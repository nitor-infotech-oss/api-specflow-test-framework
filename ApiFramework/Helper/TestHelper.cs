using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework.Helper
{
    public class TestHelper
    {
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
    }
}
