using System;
using System.Text.RegularExpressions;
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
            if (!IsValid())
                _value = input?.Trim() ?? string.Empty;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(CepType input) => input.ToString();
        public static implicit operator string(CepType? input) => input?.ToString();

        public static implicit operator CepType(string input) => new CepType(input);
        public static implicit operator CepType?(string input) => input == null ? (CepType?)null : new CepType(input);

        /// <summary>
        /// Return value 00000-000
        /// </summary>
        public static readonly CepType Empty = new CepType { _value = "00000-000" };

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
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{5}[\- ]?\d{3}$";
                if (Regex.IsMatch(input, pattern))
                {
                    input = Regex.Replace(input, @"[^\d]", string.Empty);
                    pattern = @"^(\d{5})(\d{3})$";
                    
                    output = new CepType
                    {
                        _value = Regex.Replace(input, pattern, "$1-$2"),
                        _isValid = true
                    };
                    return true;
                }
            }
            output = Empty;
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
            format = format?.Trim()?.ToUpper();

            if (format == null || format == string.Empty)
                format = "D";

            if (format.Length != 1)
                throw new ArgumentException(
                    nameof(format), Resource.TheValueIsNotValid);

            char check = format[0];

            switch (check)
            {
                case 'D':
                    return _value;

                case 'N':
                    return Regex.Replace(_value, @"[^\d]", string.Empty);

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
            return _value == other._value;
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
            if (obj is null)
            {
                return 1;
            }

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
    }
}
