using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace StockMarketSimulator.Utilities
{
    interface iQuandlRequest
    {
        dynamic Execute();
    }

    public abstract class QuandlRequest : iQuandlRequest
    {
        protected string queryString { get; set; }
        protected HttpWebRequest request { get; set; }
        protected HttpWebResponse response { get; set; }
        protected string rawJson { get; set; }
        protected JObject json { get; set; }

        protected object HttpJsonRequest ()
        {
            request = (HttpWebRequest)WebRequest.Create(queryString);
            response = (HttpWebResponse)request.GetResponse();
            rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();

            json = JObject.Parse(rawJson);

            string jsonString = json.ToString();

            return JsonConvert.DeserializeObject(jsonString);

        }

        public abstract dynamic Execute();
    }

    public class QR_DataSetRequest : QuandlRequest
    {
        public QR_DataSetRequest ()
        {
            queryString = "https://www.quandl.com/api/v3/datasets.json?database_code=NSE&sort_by=dataset_code&page=2&api_key=KRx_sFwof7iJVRtbyoE1";

        }

        public override dynamic Execute()
        {
            return GetDataSet_Dictionary();
        }


        public Dictionary<string, string> GetDataSet_Dictionary()
        {
            var entries = new Dictionary<string, string>();
            dynamic dynObj = HttpJsonRequest();

            foreach (var item in dynObj.datasets)
            {
                string company_name = item.name;
                string company_id = item.dataset_code;

                entries[company_id] = company_name;

            }

            return entries;
        }
    }
}