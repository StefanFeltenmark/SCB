namespace SCB.Domain
{
    public class SCBColumn
    {
        // dto properties
        public string code;
        public string text;
        public string type;
        public string unit;
        public string comment;
        // end dto properties

        public int id;


        public override string ToString()
        {
            return $"{text} [{type}]";
        }

        public bool IsTime()
        {
            return type.Equals("t");
        }

        public bool IsInput()
        {
            return type.Equals("d") || string.IsNullOrEmpty(type);
        }

        public bool IsOutput()
        {
            return type.Equals("c");
        }

    }
}
