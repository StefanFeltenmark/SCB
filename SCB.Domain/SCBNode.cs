namespace SCB.Domain
{
    public class SCBNode
    {
        public string id;
        public string type;
        public string text;
     //   public DateTime updated;
        public string path;

        public override string ToString()
        {
            return $"{id} [{type}]";
        }

        public string ParentPath()
        {
            return path.Remove(path.LastIndexOf('/'));
        }

        public bool IsLevel => type.Equals("l");

        public bool IsTable => type.Equals("t");

        public bool IsHeader => type.Equals("h");
    }
}