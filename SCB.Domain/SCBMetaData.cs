using System.Collections.Generic;

namespace SCB.Domain
{
    public class SCBMetaData
    {
        public string title;
        public List<SCBVariable> variables;
        public Dictionary<string, SCBVariable> _keyToVariable;

        public SCBMetaData()
        {
            _keyToVariable = new Dictionary<string, SCBVariable>();
        }

        public void Setup()
        {
            foreach (SCBVariable variable in variables)
            {
                variable.Setup();
            }
            SetUpDictionary();
        }

        private void SetUpDictionary()
        {
            foreach (SCBVariable variable in variables)
            {
                _keyToVariable.Add(variable.text, variable);
            }
        }

    }
}