namespace SCB.Client
{
    public class SCBColumn
    {
        public string code;
        public string text;
        public string type;
        public string unit;
        public string comment;

        public override string ToString()
        {
            return $"{text} [{type}]";
        }

    }
}
