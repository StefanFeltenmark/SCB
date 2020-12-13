using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCB.Domain
{
    
    public class SCBKey : IEquatable<SCBKey>
    {
        public List<SCBValue> _values;
        private string _stringRepresentation;

        public SCBKey(List<SCBValue> values)
        {
            _values = values;

            CreateStringRepresentation();
        }

        private void CreateStringRepresentation()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerator<SCBValue> enumer = _values.GetEnumerator();
            if (enumer.MoveNext())
            {
                string str = enumer.Current.value;
                string str2 = enumer.Current.valueText;
                if (!string.IsNullOrEmpty(str2))
                    sb.Append(str2);
                else if (!string.IsNullOrEmpty(str))
                    sb.Append(str);

                while (enumer.MoveNext())
                {
                    str = enumer.Current.value;
                    str2 = enumer.Current.valueText;
                    if (!string.IsNullOrEmpty(str2))
                        sb.Append(str2);
                    else if (!string.IsNullOrEmpty(str))
                        sb.Append(str);
                }
            }

            _stringRepresentation = sb.ToString();
        }


        public bool Equals(SCBKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _values.SequenceEqual(other._values);
        }

      
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SCBKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var value in _values)
                {
                    hash = hash * 31 + value.GetHashCode();
                }
                return hash;
            }
        }

        public override string ToString()
        {
            return _stringRepresentation;
        }
    }
}
