using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CSharpSelFramework.utilities
{
    public class JsonReader
    {
        public JsonReader()
        {

        }

        public string extractData(string tokenName)
        {
            var myJsonString = File.ReadAllText("utilities/testData.json");

            var jsonObject= JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();

        }

        public string [] extractDataArray(string tokenName)
        {
            var myJsonString = File.ReadAllText("utilities/testData.json");

            var jsonObject = JToken.Parse(myJsonString);

            List<string> productList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();

        }


    }

    
}
