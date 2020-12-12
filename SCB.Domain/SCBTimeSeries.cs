using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.Domain
{
    public class SCBTimeSeries : IEnumerable<KeyValuePair<DateTime,double>>
    {
        private Dictionary<DateTime, double> _values;
        private SCBKey _key;
        public double _defaultValue;
        public string _name;
        private SCBColumn _outputColumn;

        public SCBTimeSeries(SCBKey key, SCBColumn output, double defaultValue = 0.0)
        {
            _key = key;
            OutputColumn = output;
            _values = new Dictionary<DateTime, double>();
            _defaultValue = defaultValue;

            _name = _key.ToString() + "/" + _outputColumn.text;

        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public SCBColumn OutputColumn
        {
            get => _outputColumn;
            set => _outputColumn = value;
        }

        public void SetAt(DateTime t, double x)
        {
            if (_values.ContainsKey(t))
            {
                _values.Add(t,x);
            }
            else
            {
                _values[t] = x;
            }
        }

        public double GetAt(DateTime t)
        {
            if (_values.ContainsKey(t))
            {
                return _values[t];
            }
            else
            {
                return _defaultValue;
            }
            
        }

        public IEnumerator<KeyValuePair<DateTime, double>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
