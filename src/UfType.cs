using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Helpers;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct UfType
        : IFormattable, IComparable,
        IComparable<UfType>, IEquatable<UfType>
    {
        public UfType(string input)
        {
            TryParse(input, out UfType output);
            this = output;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(UfType input) => input.ToString();
        public static implicit operator UfType(string input) => new UfType(input);

        public static UfType Parse(string input)
        {
            if (TryParse(input, out UfType result))
            {
                return result;
            }
            else
            {
                if (input == null)
                    throw new ArgumentException(
                        nameof(input), Resource.ItCannotBeNull);
                else
                    throw new ArgumentException(
                        nameof(input), Resource.ItIsNotInAValidFormat);
            }
        }

        public static bool TryParse(string input, out UfType output)
        {
            input = input?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                string value = input;
                string pattern = @"^[a-zA-Z]{2}$";
                if (Regex.IsMatch(value, pattern, RegexOptions.None, AppSettings.RegEx.TimeOut))
                {
                    output = new UfType
                    {
                        _value = value.ToUpper(),
                        _isValid = true
                    };
                    return true;
                }
            }
            output = new UfType
            {
                _value = input,
                _isValid = false
            };
            return false;
        }

        public bool IsValid() => _isValid;

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _value;
        }

        public override int GetHashCode()
        {
            return $"{_value}:{GetType()}".GetHashCode();
        }

        public bool Equals(UfType other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is UfType phone && Equals(phone);
        }

        public int CompareTo(UfType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj is UfType phone)
            {
                return CompareTo(phone);
            }

            throw new ArgumentException(
                nameof(obj), Resource.ItIsNotAValidType);
        }

        public static bool operator ==(UfType left, UfType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UfType left, UfType right)
        {
            return !(left == right);
        }

        public static bool operator >(UfType left, UfType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(UfType left, UfType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(UfType left, UfType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(UfType left, UfType right)
        {
            return left < right || left == right;
        }
    }
}
