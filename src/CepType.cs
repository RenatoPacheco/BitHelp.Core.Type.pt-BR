using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct CepType
        : IFormattable, IComparable,
        IComparable<CepType>, IEquatable<CepType>, IConvertible
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
                if (Regex.IsMatch(value, pattern, RegexOptions.None, Config.RegEx.TimeOut))
                {
                    value = Regex.Replace(value, @"[^\d]", string.Empty, RegexOptions.None, Config.RegEx.TimeOut);
                    pattern = @"^(\d{5})(\d{3})$";

                    output = new CepType
                    {
                        _value = Regex.Replace(value, pattern, "$1-$2", RegexOptions.None, Config.RegEx.TimeOut),
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
                    return Regex.Replace(_value, @"[^\d]", string.Empty, RegexOptions.None, Config.RegEx.TimeOut);

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

        #region IConvertible implementation

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        /// <internalonly/>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return _value;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_value);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_value);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_value);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_value);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_value);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_value);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_value);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_value);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_value);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_value);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_value);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_value);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_value);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_value);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        #endregion
    }
}
