using System.Collections.Generic;

namespace SCB.Domain
{
    public class SCBSelection
    {
        public static string allString = "all";
        public static string itemString = "item";
        public static string topString = "top";

        private SCBVariable _variable; // Values list is chaecked/unchecked
        
        // DTO properties
        public string filter;
        public List<string> values = new List<string>();
        // end DTO props

        
        public SCBSelection(SCBVariable variable)
        {
            _variable = variable;
        }


        public void UpdateSelectedItems()
        {
            values.Clear();
            foreach (SCBValue value in _variable.Values)
            {
                if (value.IsSelected)
                {
                    values.Add(value.value);
                }
            }
        }
    }
}
