﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct DateType
        : IFormattable, IComparable,
        IComparable<DateType>, IEquatable<DateType>, IConvertible
    {
        public DateType(string input)
        {
            TryParse(input, out DateType output);
            this = output;
            if (!IsValid())
                _value = input?.Trim() ?? string.Empty;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(DateType input) => input.ToString();
        public static implicit operator DateType(string input) => new DateType(input);

        /// <summary>
        /// Return value dd/mm/aaaa
        /// </summary>
        public static readonly DateType Empty = new DateType { _value = "dd/mm/aaaa" };

        public static void Parse(string input, out DateType output)
        {
            if (TryParse(input, out DateType result))
            {
                output = result;
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
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{1,2}[\/\-]?\d{1,2}[\/\-]?\d{4}$";
                if (Regex.IsMatch(input, pattern))
                {
                    input = Regex.Replace(input, @"[^\d]", string.Empty);
                    pattern = @"^(\d{1,2})(\d{1,2})(\d{4})$";
                    input = Regex.Replace(input, pattern, "$1/$2/$3");
                    
                    if(DateTime.TryParse(input,
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
            output = Empty;
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
            return _value == other._value;
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
            if (obj is null)
            {
                return 1;
            }

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
