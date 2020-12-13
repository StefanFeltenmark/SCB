using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;

namespace SCB.Domain
{
    public class SCBTable
    {
        // Begin DTO definition
        public List<SCBColumn> columns;  // input variables, time (type = "t"), output variables (type = "c")
        public List<SCBComment> comments;
        public List<SCBTableEntry> data;
        // end DTO def

       

        public List<SCBTimeSeries> _timeSeries;

        public SCBTable()
        {
            
        }

     

        public void Setup()
        {
            int index = 0;
            foreach (SCBColumn column in columns)
            {
                column.id = index++;
            }

            List<SCBColumn> keyColumns = columns.Where(s=>s.IsInput()).Select(p=>p).ToList();
            SCBColumn timeColumn = columns.Find(s => s.IsTime());
            List<SCBColumn> valueColumns  = columns.Where(s => s.IsOutput()).Select(p => p).ToList();


            // get keys
            HashSet<SCBKey> keys = new HashSet<SCBKey>();
            Dictionary<SCBKey, List<SCBTableEntry>> entries = new Dictionary<SCBKey, List<SCBTableEntry>>();
            foreach (SCBTableEntry tableEntry in data)
            {
                List<SCBValue> vals = new List<SCBValue>();
                foreach (SCBColumn keyColumn in keyColumns)
                {
                    //string code = tableEntry.key[keyColumn.id];
                    //SCBVariable var = _metaData._keyToVariable[code];
                    //int ind = var.values.IndexOf(code);
                    //string text = var.valueTexts[ind];

                    SCBValue val = new SCBValue(tableEntry.key[keyColumn.id], "");
                    vals.Add(val);
                }

                
                SCBKey key = new SCBKey(vals);
                bool isNew = keys.Add(key);

                if (isNew)
                {
                    List<SCBTableEntry> list = new List<SCBTableEntry>();
                    list.Add(tableEntry);
                    entries.Add(key, list);
                }
                else
                {
                    entries[key].Add(tableEntry);
                }
            }


            // get time series
            _timeSeries = new List<SCBTimeSeries>();
            int offset = keyColumns.Count + 1;
            foreach (SCBKey key in keys)
            {
                foreach (SCBColumn column in valueColumns)
                {
                    SCBTimeSeries ts = new SCBTimeSeries(key, column, 0);

                    foreach (SCBTableEntry tableEntry in entries[key])
                    {
                        string timeStr = tableEntry.key[timeColumn.id];
                        DateTime dt;

                        string pattern1 = "yyyy";
                        bool parsedTimeOK = DateTime.TryParseExact(timeStr,pattern1, null,DateTimeStyles.None, out dt);

                        if (!parsedTimeOK)
                        {
                            // try format yyyyMmm
                            string yearStr = timeStr.Substring(0, 4);
                            string mString = timeStr.Substring(4, 1);
                            string monthString = timeStr.Substring(5, 2);

                            if (mString.ToUpper().Equals("M"))
                            {
                                int year = int.Parse(yearStr);
                                int month = int.Parse(monthString);
                                dt = new DateTime(year,month,1);
                            }

                            parsedTimeOK = true;

                        }

                        if (parsedTimeOK)
                        {
                            string valStr = tableEntry.values[column.id - offset];
                            double val = GetDouble(valStr, 0);

                          //  bool isNumber = double.TryParse(valStr, out val);
                            
                                ts.SetAt(dt, val);
                            
                        }
                    }
                    _timeSeries.Add(ts);
                }
                
            }
            
        }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }


    }
}
