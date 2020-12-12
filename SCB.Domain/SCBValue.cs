using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.Domain
{
    public class SCBValue : IEquatable<SCBValue>
    {
        public string value;
        public string valueText;
        private bool _isSelected;

        public SCBValue(string valueArg, string valueTextArg)
        {
            value = valueArg;
            valueText = valueTextArg;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => _isSelected = value;
        }

        public override string ToString()
        {
            return valueText;
        }

        public bool Equals(SCBValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return value == other.value && valueText == other.valueText && _isSelected == other._isSelected;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SCBValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (value != null ? value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (valueText != null ? valueText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _isSelected.GetHashCode();
                return hashCode;
            }
        }
    }
}
