using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Helpers
{
    public class Convertions
    {
        public static double Todouble(string str)
        {
            double d = 0;
            if (str == null || str == "" || str == "NaN")
            {
                return 0;
            }
            else
            {
                try
                {
                    //if (str.Contains('.'))
                    //{
                    //   str = str.Replace('.', '0'); 
                    //}
                    d = System.Convert.ToDouble(str);
                }
                catch (Exception exc)
                {
                    return 0;
                }
            }
            return d;
        }

        public static double Todouble(object obj)
        {
            double d = 0;
            if (obj == null || obj.ToString() == "" || obj == "NaN")
            {
                return 0;
            }
            else
            {
                try
                {
                    d = System.Convert.ToDouble(obj.ToString());
                }
                catch (Exception exc)
                {
                    return 0;
                }
            }
            return d;
        }

        public static int ToInt(string str)
        {
            int i = 0;
            if (str == null || str == "")
            {
                return 0;
            }
            else
            {
                try
                {
                    i = System.Convert.ToInt32(str);
                }
                catch (Exception exc)
                {
                    return 0;
                }
            }
            return i;

        }
        public static long ToLong(string str)
        {
            long i = 0;
            if (str == null || str == "")
            {
                return 0;
            }
            else
            {
                try
                {
                    i = System.Convert.ToInt64(str);
                }
                catch (Exception exc)
                {
                    return 0;
                }
            }
            return i;

        }


        public static int ToInt(object obj)
        {
            int i = 0;
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                try
                {
                    i = System.Convert.ToInt32(obj);
                }
                catch (Exception exc)
                {
                    return 0;
                }
            }
            return i;

        }
        public static string ObjToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString(); 
            }

        }

        public static object EnglishNumber2Persian(object obj)
        {
            string persianNumber = "";
            foreach (char ch in obj.ToString())
            {
                char c;
                if (char.IsDigit(ch))
                {
                    c = (char)(1776 + char.GetNumericValue(ch));

                }
                else if (ch == '.')
                {
                    c = (char)47;
                }
                else
                {
                    c = ch;
                }

                persianNumber += c;
            }
            return persianNumber;
        }

        public static object numPer2numEn(object obj)
        {
            string perNumber =(string)obj;
            string englishNumber = "";
            foreach (char ch in perNumber)
            {
                if (char.IsDigit(ch))
                {
                    if ((char)ch == 47)
                    {
                        englishNumber += (char)46;
                        continue;
                    }
                    double num = char.GetNumericValue(ch);
                    englishNumber += num;

                }
            }
            return englishNumber;
        }

    }
}
