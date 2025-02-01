using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace C_SeleniumFrameWork.Utilities
{
    class JsonReader
    {

        public JsonReader()
        {

        }

        public string ExtractData(String tokenName)
        {
            var jsonString = File.ReadAllText("Utilities/TestData.json");
            var jsonObject = JToken.Parse(jsonString);

            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public String[] ExtractDataArray(String tokenName)
        {
            var jsonString = File.ReadAllText("Utilities/TestData.json");
            var jsonObject = JToken.Parse(jsonString);

            List<string> phoneList =  jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return phoneList.ToArray();
        }
    }

    
}
