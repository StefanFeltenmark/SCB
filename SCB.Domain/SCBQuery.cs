using System.Collections.Generic;

namespace SCB.Domain
{
    public class SCBQuery
    {
        public enum Filter
        {
            all,
            item,
            top,
            agg,
            vs
        }

        // DTO props
        public List<SCBQueryItem> query = new List<SCBQueryItem>();
        public SCBResponse response = new SCBResponse("json");
        // end DTO props

        public Dictionary<string, SCBQueryItem> _items;

        public SCBQuery()
        {
            _items = new Dictionary<string, SCBQueryItem>();
        }

        public void SetUp(List<SCBVariable> variables)
        {
            foreach (SCBVariable variable in variables)
            {
                SCBQueryItem item = new SCBQueryItem(variable)
                {
                    code = variable.code, selection = {filter = SCBSelection.allString}
                };

                // default is o select all
                item.selection.values.Add("*");
                _items.Add(variable.code, item);
                query.Add(item);
            }
        }

        public void Update()
        {
            foreach (SCBQueryItem queryItem in query)
            {
                if (queryItem.selection.filter.Equals(SCBSelection.itemString))
                {
                    queryItem.selection.UpdateSelectedItems();
                }
            }
        }

        public void SetSelection(SCBVariable var, Filter filter, List<string> items, int top = 10)
        {
            SCBQueryItem item = _items[var.code];
            if (item != null)
            {
                switch (filter)
                {
                    case Filter.all:
                        item.selection.filter = SCBSelection.allString;
                        item.selection.values.Clear();
                        item.selection.values.Add("*");
                        break;
                    case Filter.item:
                        item.selection.filter = SCBSelection.itemString;
                        item.selection.values.Clear();
                        item.selection.values.AddRange(items);
                        break;
                    case Filter.top:
                        item.selection.filter = SCBSelection.topString;
                        item.selection.values.Clear();
                        item.selection.values.Add(top.ToString());
                        break;
                    default:
                        item.selection.filter = SCBSelection.allString;
                        item.selection.values.Clear();
                        item.selection.values.Add("*");
                        break;
                }
            }
        }
    }
}
