using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.Client;
using SCB.Domain;

namespace SCB.Controller
{
    
    public class SCBController
    {
        private SCBClient _client;
        private static string _api = "api.scb.se/OV0104";
        private SCBNode _current;

        public SCBController()
        {
            _client = new SCBClient(_api);
            Current = _client.TopNode;

        }

        public SCBNode Current
        {
            get => _current;
            set => _current = value;
        }

        public async Task<List<SCBNode>> GetChildren(SCBNode node)
        {
            return await _client.GetNodesBelow(node);
        }

        public async Task<SCBMetaData> GetMetaData(SCBNode node)
        {
            return await _client.GetMetaData(node.path);
        }

        public async Task<SCBTable> GetTable(SCBNode node, SCBQuery query)
        {
            return await _client.GetTable(node, query);
        }

    }
}
