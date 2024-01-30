using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Helpers;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct DateType
        : IFormattable, IComparable,
        IComparable<DateType>, IEquatable<DateType>
    {
        public DateType(string input)
        {
            TryParse(input, out DateType output);
            this = output;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(DateType input) => input.ToString();
        public static implicit operator DateType(string input) => new DateType(input);

        public static DateType Parse(string input)
        {
            if (TryParse(input, out DateType result))
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

        public static bool TryParse(string input, out DateType output)
        {
            input = input?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                string value = input;
                string pattern = @"^\d{1,2}[\/\-]?\d{1,2}[\/\-]?\d{4}$";
                if (Regex.IsMatch(value, pattern, RegexOptions.None, AppSettings.RegEx.TimeOut))
                {
                    value = Regex.Replace(value, @"[^\d]", string.Empty, RegexOptions.None, AppSettings.RegEx.TimeOut);
                    pattern = @"^(\d{1,2})(\d{1,2})(\d{4})$";
                    value = Regex.Replace(value, pattern, "$1/$2/$3", RegexOptions.None, AppSettings.RegEx.TimeOut);
                    
                    if(DateTime.TryParse(value,
                        CultureInfo.GetCultureInfo("pt-BR"),
                        DateTimeStyles.None, out DateTime v))
                    {
                        output = new DateType
                        {
                            _value = v.ToString("dd/MM/yyyy"),
                            _isValid = true
                        };
                        return true;
                    }
                }
            }
            output = new DateType
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

        public bool Equals(DateType other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is DateType phone && Equals(phone);
        }

        public int CompareTo(DateType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj is DateType phone)
            {
                return CompareTo(phone);
            }

            throw new ArgumentException(
                nameof(obj), Resource.ItIsNotAValidType);
        }

        public static bool operator ==(DateType left, DateType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DateType left, DateType right)
        {
            return !(left == right);
        }

        public static bool operator >(DateType left, DateType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(DateType left, DateType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(DateType left, DateType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(DateType left, DateType right)
        {
            return left < right || left == right;
        }
    }
}
