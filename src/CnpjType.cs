using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct CnpjType
        : IFormattable, IComparable,
        IComparable<CnpjType>, IEquatable<CnpjType>, IConvertible
    {
        public CnpjType(string input)
        {
            TryParse(input, out CnpjType output);
            this = output;
            if (!IsValid())
                _value = input?.Trim() ?? string.Empty;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(CnpjType input) => input.ToString();
        public static implicit operator CnpjType(string input) => new CnpjType(input);

        /// <summary>
        /// Return value 00.000.000/0000-00
        /// </summary>
        public static readonly CnpjType Empty = new CnpjType { _value = "00.000.000/0000-00" };

        public static CnpjType Parse(string input)
        {
            if (TryParse(input, out CnpjType result))
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

        public static bool TryParse(string input, out CnpjType output)
        {
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{2}[\. ]?\d{3}[\. ]?\d{3}[\/ ]?\d{4}[\- ]?\d{2}$";
                if (Regex.IsMatch(input, pattern))
                {
                    input = Regex.Replace(input, @"[^\d]", string.Empty);
                    output = GenerateDigit(input.Substring(0, 12));

                    if (output.ToString("N") == input)
                        return true;
                }
            }
            output = Empty;
            return false;
        }

        /// <summary>
        /// Generate a valid CNPJ
        /// </summary>
        /// <returns>A object CnpjType with a CNPJ valid</returns>
        public static CnpjType Generate()
        {
            string partialCnpj = string.Empty;
            for (int i = 0; i < 12; i++)
                partialCnpj += new Random().Next(0, 9).ToString();

            return GenerateDigit(partialCnpj);
        }

        private static CnpjType GenerateDigit(string partialCnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            tempCnpj = partialCnpj;
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            string pattern = @"^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$";
            return new CnpjType
            {
                _value = Regex.Replace(
                    partialCnpj + digito,
                    pattern, "$1.$2.$3/$4-$5"),
                _isValid = true
            };
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

        public bool Equals(CnpjType other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is CnpjType phone && Equals(phone);
        }

        public int CompareTo(CnpjType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is CnpjType phone)
            {
                return CompareTo(phone);
            }

            throw new ArgumentException(
                nameof(obj), Resource.ItIsNotAValidType);
        }

        public static bool operator ==(CnpjType left, CnpjType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CnpjType left, CnpjType right)
        {
            return !(left == right);
        }

        public static bool operator >(CnpjType left, CnpjType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(CnpjType left, CnpjType right)
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
