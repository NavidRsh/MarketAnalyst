using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketAnalyst.Core.Helpers
{
   public  class PersianDate
    {
       public static string GeorgianToPersian(DateTime? dateTime)
       {
           if (dateTime == null || dateTime.Value == null )
           {
               return "";
           }
           System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
           string Year = PC.GetYear(dateTime.Value).ToString();
           string Month = PC.GetMonth(dateTime.Value).ToString();
           string Day = PC.GetDayOfMonth(dateTime.Value).ToString();
           if (Month.Length == 1)
           {
               Month = "0" + Month;
           }
           if (Day.Length == 1)
           {
               Day = "0" + Day;
           }
           return Year + "/" + Month + "/" + Day;
       }
        public static string GeorgianToPersian(DateTime dateTime)
        {
            if (dateTime == null)
            {
                return ""; 
            }
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            string Year = PC.GetYear(dateTime).ToString();
            string Month = PC.GetMonth(dateTime).ToString();
            string Day = PC.GetDayOfMonth(dateTime).ToString();
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            if (Day.Length == 1)
            {
                Day = "0" + Day;
            }
            return Year + "/" + Month + "/" + Day;
        }


        public static DateTime PersianToGergian(string dateString)
        {
            try
            {
                char[] WordSeprator = new char[] { '/' };
                string[] Persiandate = dateString.Split(WordSeprator, StringSplitOptions.RemoveEmptyEntries);
                System.Globalization.PersianCalendar GergianDate = new System.Globalization.PersianCalendar();
                return GergianDate.ToDateTime(int.Parse(Persiandate[0]), int.Parse(Persiandate[1]), int.Parse(Persiandate[2]), 1, 1, 1, 1, System.Globalization.GregorianCalendar.ADEra);
            }
            catch(Exception exc)
            {
                return DateTime.MinValue; 
            }

        }

        public static DateTime PersianToGergian(string dateString, string time)
        {
            if (time.Length == 3)
            {
                time = "0" + time; 
            }

            char[] WordSeprator = new char[] { '/' };
            string[] Persiandate = dateString.Split(WordSeprator, StringSplitOptions.RemoveEmptyEntries);

            char[] timeSeprator = new char[] { ':' };
            
            //string[] timeParts = time.Split(timeSeprator, StringSplitOptions.RemoveEmptyEntries);

            int hour = 0; 
            int min = 0 ; 
            int sec = 0; 
            
            //if ( timeParts.Length > 0 && timeParts[0] != null)
            //{
            //    hour = int.Parse(timeParts[0]); 
            //}
            //if (timeParts.Length > 1 && timeParts[1] != null)
            //{
            //    min = int.Parse(timeParts[1]); 
            //}
            //if (timeParts.Length > 2 && timeParts[2] != null)
            //{
            //    sec = int.Parse(timeParts[2]); 
            //}
            if (time.Length > 1)
            {
                hour = int.Parse(time.Substring(0, 2));
            }
            if (time.Length > 3)
            {
                min = int.Parse(time.Substring(2));
            }
            System.Globalization.PersianCalendar GergianDate = new System.Globalization.PersianCalendar();
            return GergianDate.ToDateTime(int.Parse(Persiandate[0]), int.Parse(Persiandate[1]), int.Parse(Persiandate[2]), hour, min, sec, 0, System.Globalization.GregorianCalendar.ADEra);
        }

        public static string GeorgianToPersianWithTime(DateTime dateTime)
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            string Year = PC.GetYear(dateTime).ToString();
            string Month = PC.GetMonth(dateTime).ToString();
            string Day = PC.GetDayOfMonth(dateTime).ToString();
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            if (Day.Length == 1)
            {
                Day = "0" + Day;
            }
            string Hour = dateTime.Hour.ToString();
            string Min = dateTime.Minute.ToString();
            string Seconds = dateTime.Second.ToString();

            return Year + "/" + Month + "/" + Day + " " + Hour.PadLeft(2, '0') + ":" + Min.PadLeft(2, '0') + ":" + Seconds.PadLeft(2, '0');
        }

    }
}
