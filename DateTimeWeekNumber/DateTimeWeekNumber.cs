using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHiDateTimeWeekNumber
{
  public class DateTimeWeekNumber
  {
    /// <summary>
    /// Day number of the week (1 = Monday, 7 = Sunday).
    /// </summary>
    public int DayNo { get; private set; }

    /// <summary>
    /// Week number of the year.
    /// </summary>
    public int WeekNo { get; private set; }

    /// <summary>
    /// Year can be corrected.
    /// </summary>
    public int YearNo { get; private set; }

    public string DayNoString
    {
      get
      {
        return DayNo.ToString();
      }
    }

    public string WeekNoString
    {
      get
      {
        return WeekNo.ToString();
      }
    }

    public string WeekNoString2
    {
      get
      {
        return $"0{WeekNoString}".Substring(WeekNoString.Length - 1);
      }
    }

    public string YearNoString
    {
      get
      {
        return YearNo.ToString();
      }
    }

    public string YearNoString2
    {
      get
      {
        return YearNoString.Substring(2, 2);
      }
    }

    public string ISOWeekNoExtended
    {
      get
      {
        return $"{YearNoString}-W{WeekNoString2}-{DayNoString}";
      }
    }

    public string ISOWeekNoCompact
    {
      get
      {
        return $"{YearNoString}W{WeekNoString2}{DayNoString}";
      }
    }

    public string WeekNoExtended
    {
      get
      {
        return $"{YearNoString}{WeekNoString2}{DayNoString}";
      }
    }

    public string WeekNoCompact
    {
      get
      {
        return $"{YearNoString2}{WeekNoString2}{DayNoString}";
      }
    }

    private static DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
    private static Calendar cal = dfi.Calendar;

    public DateTimeWeekNumber(DateTime date)
    {
      int year = date.Year;
      int week = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

      if (week == 52 && date.Month == 1)
      {
        year--;
      }
      else if (week == 53)
      {
        if (date.Month == 12 && date.DayOfWeek <= DayOfWeek.Wednesday)
        {
          week = 1;
          year++;
        }
        else if (date.Month == 1 && date.DayOfWeek >= DayOfWeek.Thursday)
        {
          year--;
        }
      }

      DayNo = (int)date.DayOfWeek;
      if (DayNo == 0)
      {
        DayNo = 7;
      }
      WeekNo = week;
      YearNo = year;
    }

    public override string ToString()
    {
      return ISOWeekNoExtended;
    }

    public static DateTime? WeekNoCompactToDate(string reference)
    {
      if (string.IsNullOrEmpty(reference))
      {
        return null;
      }

      int Year = -1;
      int.TryParse(reference.Substring(0, 2), out Year);
      if (Year == -1)
      {
        return null;
      }

      Year = Year + 2000;

      int Week = -1;
      int.TryParse(reference.Substring(2, 2), out Week);
      if (Week == -1)
      {
        return null;
      }

      int Day = -1;
      int.TryParse(reference.Substring(4, 1), out Day);
      if (Day == -1)
      {
        return null;
      }

      DateTime ThisYear = new DateTime(Year, 1, 1);
      DateTimeWeekNumber dwn = new DateTimeWeekNumber(ThisYear);

      //Begin of the week
      ThisYear = ThisYear.AddDays(1 - dwn.DayNo);

      int Days = (Week - 1) * 7 + (Day - 1);

      return ThisYear.AddDays(Days);
    }
  }
}
