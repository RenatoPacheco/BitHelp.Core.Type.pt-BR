using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Type.pt_BR.Helpers;
using BitHelp.Core.Type.pt_BR.Resources;

namespace BitHelp.Core.Type.pt_BR
{
    public struct CnpjType
        : IFormattable, IComparable,
        IComparable<CnpjType>, IEquatable<CnpjType>
    {
        public CnpjType(string input)
        {
            TryParse(input, out CnpjType output);
            this = output;
        }

        private string _value;
        private bool _isValid;

        public static implicit operator string(CnpjType input) => input.ToString();
        public static implicit operator CnpjType(string input) => new CnpjType(input);

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
            input = input?.Trim() ?? string.Empty;
            string value = input;
            if (!string.IsNullOrEmpty(value))
            {
                string pattern = @"^\d{2}[\. ]?\d{3}[\. ]?\d{3}[\/ ]?\d{4}[\- ]?\d{2}$";
                if (Regex.IsMatch(value, pattern, RegexOptions.None, AppSettings.RegEx.TimeOut))
                {
                    value = Regex.Replace(value, @"[^\d]", string.Empty, RegexOptions.None, AppSettings.RegEx.TimeOut);
                    output = GenerateDigit(value.Substring(0, 12));

                    if (output.ToString("N") == value)
                    {
                        output._isValid = true;
                        return true;
                    }
                }
            }
            output = new CnpjType
            {
                _value = input,
                _isValid = false
            };
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
                    pattern, "$1.$2.$3/$4-$5", 
                    RegexOptions.None, 
                    AppSettings.RegEx.TimeOut),
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

        public bool Equals(CnpjType other)
        {
            return GetHashCode() == other.GetHashCode();
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

        public static bool operator >=(CnpjType left, CnpjType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(CnpjType left, CnpjType right)
        {
            return left < right || left == right;
        }
    }
}
