using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SCB.Domain;

namespace SCB.Client
{
    public class SCBClient
    {
        string _apiName = @"api.scb.se/OV0104";
        string _apiVersion = "v1/doris";
        string _apiLanguage = "sv";
        private string _apiScheme = "http";
        private SCBDataBase _dataBase;
        private SCBNode _topNode;
        private SCBNode _currentNode;

        private static readonly HttpClient client = new HttpClient();

        public SCBClient(string apiName)
        {
            _apiName = apiName;
            
            TopNode = new SCBNode
            {
                path = "http://api.scb.se/OV0104/v1/doris/en/ssd",
                type = "l",
                id = "ssd",
                text = "SCBRoot"
            };
            CurrentNode = TopNode;
        }

        public SCBNode CurrentNode
        {
            get => _currentNode;
            set => _currentNode = value;
        }

        public SCBNode TopNode
        {
            get => _topNode;
            set => _topNode = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadTable()
        {
            //API - NAME / API - VERSION / LANGUAGE / DATABASE - ID /< LEVELS >/ TABLE - ID
            string dataBaseName = "ssd";

            string level = "BE";
            
            string tableId = "BE0401";

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(_apiScheme);
            strBuilder.Append("://");
            strBuilder.Append(_apiName);
            strBuilder.Append("/");
            strBuilder.Append(_apiVersion);
            strBuilder.Append("/");
            strBuilder.Append(_apiLanguage);
            strBuilder.Append("/");
            strBuilder.Append(dataBaseName);
            strBuilder.Append("/");
            strBuilder.Append(level);
            strBuilder.Append("/");
            strBuilder.Append(tableId);
            
            UriBuilder urb = new UriBuilder();
            urb.Host = _apiName;
            urb.Path = "";
            urb.Scheme = "http";
            urb.Query = "";
            urb.Fragment = "frag";

            string test  = urb.Uri.ToString();

            string url = strBuilder.ToString();
            
            var data = GetChildNodes(url);
        }

        private async Task<List<SCBNode>> GetChildNodes(string url)
        {
            List<SCBNode> data = new List<SCBNode>();
            var html = GetRequest(url).Result;
            if (string.IsNullOrEmpty(html)) return null;
            data = JsonConvert.DeserializeObject<List<SCBNode>>(html);
            foreach (SCBNode scbNode in data)
            {
                scbNode.path = url + "/" + scbNode.id;
            }
            return data;
        }

        private async Task<string> GetRequest(string url)
        {
            //return await client.GetStringAsync(url);

             var response = await client.GetAsync(url).ConfigureAwait(false);
             if (response.IsSuccessStatusCode)
             {
                return response.Content.ReadAsStringAsync().Result;
             }
             else
             {
                 return string.Empty;
             }
        }

        public async Task<SCBMetaData> GetMetaData(string url)
        {
            var rp = await GetRequest(url);
            SCBMetaData table = JsonConvert.DeserializeObject<SCBMetaData>(rp);
            return table;
        }

        private async Task<string> PostRequest(string url, string jsonQueryString)
        {
            var resp = await client.PostAsync(url, new StringContent(jsonQueryString, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode) return string.Empty;
            var responseString = await resp.Content.ReadAsStringAsync();
            return responseString;
        }


        public async Task<SCBTable> GetTable(SCBNode parent, SCBQuery query)
        {
            string queryString = JsonConvert.SerializeObject(query);

            var html = PostRequest(parent.path, queryString).Result;
            
            //File.WriteAllText(".\\temp.json", html);

            var data = !html.Equals( String.Empty) ? JsonConvert.DeserializeObject<SCBTable>(html) : new SCBTable();

            return data;
        }

        public async Task<List<SCBNode>> GetNodesBelow(SCBNode parent)
        {
            List<SCBNode> nodes = new List<SCBNode>();
            if (parent.type.Equals("l"))
            {
                nodes = await GetChildNodes(parent.path);
            }
            return nodes;
        }


    }
}
