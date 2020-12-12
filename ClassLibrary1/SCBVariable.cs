using System.Collections.Generic;

namespace SCB.Client
{
    public class SCBVariable
    {
        public string code;
        public string text;
        public List<string> values = new List<string>();
        public List<string> valueTexts = new List<string>();
        public bool elimination;
        public bool time;

        public override string ToString()
        {
            return $"{code}";
        }
    }
}