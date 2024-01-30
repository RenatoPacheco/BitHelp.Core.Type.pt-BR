using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Helpers;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct DateTimeType
        : IFormattable, IComparable,
        IComparable<DateTimeType>, IEquatable<DateTimeType>
    {
        public DateTimeType(string input)
        {
            TryParse(input, out DateTimeType output);
            this = output;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(DateTimeType input) => input.ToString();
        public static implicit operator DateTimeType(string input) => new DateTimeType(input);

        public static DateTimeType Parse(string input)
        {
            if (TryParse(input, out DateTimeType result))
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

        public static bool TryParse(string input, out DateTimeType output)
        {
            input = input?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                string value = input;
                string pattern = @"^\d{1,2}[\/\-]?\d{1,2}[\/\-]?\d{4}";
                IFormatProvider provider = CultureInfo.GetCultureInfo("pt-BR");
                if (Regex.IsMatch(value, pattern, RegexOptions.None, AppSettings.RegEx.TimeOut))
                {
                    pattern = @"[\/\-]+";
                    value = Regex.Replace(value, pattern, "/", RegexOptions.None, AppSettings.RegEx.TimeOut);
                    
                    if(DateTime.TryParse(value, provider,
                        DateTimeStyles.None, out DateTime v))
                    {
                        output = new DateTimeType
                        {
                            _value = v.ToString("dd/MM/yyyy HH:mm:ss", provider),
                            _isValid = true
                        };
                        return true;
                    }
                }
            }
            output = new DateTimeType
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

        public bool Equals(DateTimeType other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is DateTimeType phone && Equals(phone);
        }

        public int CompareTo(DateTimeType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj is DateTimeType phone)
            {
                return CompareTo(phone);
            }

            throw new ArgumentException(
                nameof(obj), Resource.ItIsNotAValidType);
        }

        public static bool operator ==(DateTimeType left, DateTimeType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DateTimeType left, DateTimeType right)
        {
            return !(left == right);
        }

        public static bool operator >(DateTimeType left, DateTimeType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(DateTimeType left, DateTimeType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(DateTimeType left, DateTimeType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(DateTimeType left, DateTimeType right)
        {
            return left < right || left == right;
        }
    }
}
