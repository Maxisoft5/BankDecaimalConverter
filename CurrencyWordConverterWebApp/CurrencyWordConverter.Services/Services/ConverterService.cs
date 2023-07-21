using CurrencyWordConverter.Services.Interfaces;
using System.Text;

namespace CurrencyWordConverter.Services.Services
{
    public sealed class ConverterService : IConverterService
    {
        private readonly Dictionary<int, string> _physicalValueByNumber = new()
        {
            {1, "one" },
            {2, "two" },
            {3, "three" },
            {4, "four" },
            {5, "five" },
            {6, "six" },
            {7, "seven" },
            {8, "eight" },
            {9, "nine" },
            {10, "ten" },
            {11, "eleven" },
            {12, "twelve" },
            {13, "thirteen" },
            {14, "fourteen" },
            {15, "fiveteen" },
            {16, "sixteen" },
            {17, "seventeen" },
            {18, "eighteen" },
            {19, "nineteen" },
            {20, "twenty" },
            {30, "thirty" },
            {40, "fourty" },
            {50, "fifty" },
            {60, "sixty" },
            {70, "seventy" },
            {80, "eighty" },
            {90, "ninety" }
        };

        private readonly Dictionary<string, string> _fullCurrenciesNames = new()
        {
            {"USD", "DOLLARS" },
            {"RUB", "RUBLES" },
            {"EUR", "EURO" }
        };

        public string GetTranslationOfDecimalToPhysical(string decimalValue, string selectedCurrency)
        {
            var splitedBySeparator = decimalValue.Split('.');
            char[] beforeSeparator = splitedBySeparator[0].ToCharArray();

            StringBuilder sb = new StringBuilder();
            int beforeLength = beforeSeparator.Length;

            for (int i = 0; i < beforeLength; i++)
            {
                int diff = beforeLength - i;
                int digit = int.Parse(beforeSeparator[i].ToString());

                switch (diff)
                {
                    #region billion
                    case 10:
                        {
                            sb.Append($"{_physicalValueByNumber[digit]} bilion, ");
                            break;
                        }
                    #endregion

                    #region millions
                    case 9:
                        {
                            if (digit == 0) break;

                            sb.Append($"{_physicalValueByNumber[digit]} hundred ");
                            var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                            if (nextDigit > 1)
                            {
                                sb.Append("and ");
                            }
                            break;

                        }
                    case 8:
                        {
                            if (digit == 0) break;
                            if (digit == 1)
                            {
                                var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}{nextDigit}")]} ");
                            }
                            else
                            {
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}0")]} ");
                            }
                            break;

                        }
                    case 7:
                        {
                            if (digit == 0)
                            {
                                if (beforeSeparator[i - 1] == '0' && beforeSeparator[i - 2] == '0')
                                {
                                    break;
                                }
                                sb.Append("million, ");
                                break;
                            }
                            else
                            {
                                if (i != 0)
                                {
                                    var previousDigit = int.Parse(beforeSeparator[i - 1].ToString());
                                    if (previousDigit == 1)
                                    {
                                        sb.Append("million, ");
                                        break;
                                    }
                                }
                                sb.Append($"{_physicalValueByNumber[digit]} ");
                                sb.Append("million, ");
                                break;
                            }
                        }
                    #endregion

                    #region thousands
                    case 6:
                        {
                            if (digit == 0) break;

                            sb.Append($"{_physicalValueByNumber[digit]} hundred ");
                            var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                            if (nextDigit > 1)
                            {
                                sb.Append("and ");
                            }
                            break;
                        }
                    case 5:
                        {
                            if (digit == 0) break;

                            if (digit == 1)
                            {
                                var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}{nextDigit}")]} ");
                            }
                            else
                            {
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}0")]} ");
                            }
                            break;
                        }
                    case 4:
                        {
                            if (digit == 0)
                            {
                                if (beforeSeparator[i - 1] == '0' && beforeSeparator[i - 2] == '0')
                                {
                                    break;
                                }
                                sb.Append("thousand, ");
                                break;
                            }
                            else
                            {
                                if (i != 0)
                                {
                                    var previousDigit = int.Parse(beforeSeparator[i - 1].ToString());
                                    if (previousDigit == 1)
                                    {
                                        sb.Append("thousand, ");
                                        break;
                                    }
                                }
                                sb.Append($"{_physicalValueByNumber[digit]} ");
                                sb.Append("thousand, ");
                                break;
                            }
                        }
                    #endregion
                    #region hundredes
                    case 3:
                        {
                            if (digit == 0) break;

                            sb.Append($"{_physicalValueByNumber[digit]} hundred ");
                            var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                            if (nextDigit > 1)
                            {
                                sb.Append("and ");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (digit == 0) break;


                            if (digit == 1)
                            {
                                var nextDigit = int.Parse(beforeSeparator[i + 1].ToString());
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}{nextDigit}")]} ");
                            }
                            else
                            {
                                sb.Append($"{_physicalValueByNumber[int.Parse($"{digit}0")]} ");
                            }
                            break;
                        }
                    case 1:
                        {
                            if (digit == 0) break;

                            if (i != 0)
                            {
                                var previousDigit = int.Parse(beforeSeparator[i - 1].ToString());
                                if (previousDigit == 1)
                                {
                                    break;
                                }
                            }

                            sb.Append($"{_physicalValueByNumber[digit]} ");
                            break;
                        }
                        #endregion
                }
            }
            sb = TrimEnd(sb);
            if (sb[^1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            string currency = "";
            if (_fullCurrenciesNames.TryGetValue(selectedCurrency.ToUpper(), out string existsCurrency))
            {
                currency = existsCurrency;
            } else
            {
                currency = selectedCurrency.ToUpper();
            }
          
            sb.Append($" {currency}");
            

            if (splitedBySeparator.Length > 1 && (splitedBySeparator[1][0] != '0' || splitedBySeparator[1][1] != '0'))
            {
                sb.Append(" AND ");
                if (splitedBySeparator[1][0] != '0')
                {
                    sb.Append($"{_physicalValueByNumber[int.Parse($"{splitedBySeparator[1][0]}0")]} ");
                }
                if (splitedBySeparator[1].Length > 1 && splitedBySeparator[1][1] != '0')
                {
                    sb.Append($"{_physicalValueByNumber[int.Parse($"{splitedBySeparator[1][1]}")]} ");
                }
                sb.Append("CENTS");
            }

            return sb.ToString();
        }

        private StringBuilder TrimEnd(StringBuilder sb)
        {
            if (sb == null || sb.Length == 0) return sb;

            int i = sb.Length - 1;

            for (; i >= 0; i--)
                if (!char.IsWhiteSpace(sb[i]))
                    break;

            if (i < sb.Length - 1)
                sb.Length = i + 1;

            return sb;
        }
    }
}
