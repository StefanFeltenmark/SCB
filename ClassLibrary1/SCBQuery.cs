using System.Collections.Generic;

namespace SCB.Client
{
    public class SCBQuery
    {
        public List<SCBQueryItem> query = new List<SCBQueryItem>();
        public SCBResponse response = new SCBResponse("json");
    }
}
