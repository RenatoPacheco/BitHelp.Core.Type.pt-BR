using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Helpers;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct CepType
        : IFormattable, IComparable,
        IComparable<CepType>, IEquatable<CepType>
    {
        public CepType(string input)
        {
            TryParse(input, out CepType output);
            this = output;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(CepType input) => input.ToString();
        public static implicit operator CepType(string input) => new CepType(input);

        public static CepType Parse(string input)
        {
            if (TryParse(input, out CepType result))
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

        public static bool TryParse(string input, out CepType output)
        {
            input = input?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                string value = input;
                string pattern = @"^\d{5}[\- ]?\d{3}$";
                if (Regex.IsMatch(value, pattern, RegexOptions.None, AppSettings.RegEx.TimeOut))
                {
                    value = Regex.Replace(value, @"[^\d]", string.Empty, RegexOptions.None, AppSettings.RegEx.TimeOut);
                    pattern = @"^(\d{5})(\d{3})$";

                    output = new CepType
                    {
                        _value = Regex.Replace(value, pattern, "$1-$2", RegexOptions.None, AppSettings.RegEx.TimeOut),
                        _isValid = true
                    };
                    return true;
                }
            }
            output = new CepType
            {
                _value = input,
                _isValid = false
            };
            return false;
        }

        public bool IsValid() => _isValid;

        public override string ToString()
        {
            return ToString("D", null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            format = format?.Trim().ToUpper() ?? string.Empty;

            if (format.Length != 1)
                throw new ArgumentException(
                    nameof(format), Resource.TheValueIsNotValid);

            char check = format[0];

            switch (check)
            {
                case 'D':
                    return _value;

                case 'N':
                    return Regex.Replace(_value, @"[^\d]", string.Empty, RegexOptions.None, AppSettings.RegEx.TimeOut);

                default:
                    throw new ArgumentException(
                        nameof(format), Resource.TheValueIsNotValid);
            }
        }

        public override int GetHashCode()
        {
            return $"{_value}:{GetType()}".GetHashCode();
        }

        public bool Equals(CepType other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is CepType phone && Equals(phone);
        }

        public int CompareTo(CepType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj is CepType phone)
            {
                return CompareTo(phone);
            }

            throw new ArgumentException(
                nameof(obj), Resource.ItIsNotAValidType);
        }

        public static bool operator ==(CepType left, CepType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CepType left, CepType right)
        {
            return !(left == right);
        }

        public static bool operator >(CepType left, CepType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(CepType left, CepType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(CepType left, CepType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(CepType left, CepType right)
        {
            return left < right || left == right;
        }
    }
}
