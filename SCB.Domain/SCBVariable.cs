using System.Collections.Generic;

namespace SCB.Domain
{
    public class SCBVariable
    {
        public string code;
        public string text;
        public List<string> values = new List<string>();
        public List<string> valueTexts = new List<string>();
        public bool elimination;
        public bool time;
        private List<SCBValue> _values = new List<SCBValue>();

        public SCBVariable()
        {
            
        }

        public void Setup()
        {
            for (int i = 0; i < values.Count; i++)
            {
                _values.Add(new SCBValue(values[i], valueTexts[i]));
            }
        }

        public List<SCBValue> Values
        {
            get => _values;
            set => _values = value;
        }


        public override string ToString()
        {
            return $"{code}";
        }
    }
}