using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CFMStats.Classes
{
    public static class Helper
    {
        public static bool BooleanNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return false;
            }

            if (dbvalue == null)
            {
                return false;
            }

            return Convert.ToBoolean(dbvalue);
        }

        public static double CalculateQbRating(int attempts, int completions, int touchdowns, int interceptions, int yards)
        {
            if (attempts == 0)
            {
                return 0.0;
            }

            double a, b, c, d, rating;
            a = SetMinMax((completions * 100 / attempts - 30) * 0.05);
            b = SetMinMax((yards / attempts - 3) * 0.25);
            c = SetMinMax(touchdowns * 100 / attempts * 0.2);
            d = SetMinMax(2.375 - interceptions * 100 / attempts * 0.25);

            rating = (a + b + c + d) / 6 * 100;

            Console.WriteLine("Quarterback Rating: {0}", Math.Round(rating, 1));

            return Math.Round(rating, 1);
        }

        public static DateTime DatetimeNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return DateTime.MinValue;
            }

            if (dbvalue == null)
            {
                return DateTime.MinValue;
            }

            return Convert.ToDateTime(dbvalue);
        }

        public static decimal DecimalNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return 0;
            }

            if (dbvalue == null)
            {
                return 0;
            }

            return Convert.ToDecimal(dbvalue);
        }

        public static double DoubleNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return 0.0;
            }

            return dbvalue == null ? 0.0 : Convert.ToDouble(dbvalue);
        }

        public static string GetAverage(int Total, int Value)
        {
            var sValue = string.Empty;

            if (Total == 0)
            {
                sValue = "0";
            }
            else
            {
                var average = Total / (double) Value;
                sValue = $"{average:0.##}";
            }

            return sValue;
        }

        /// <summary>
        ///     Return the percentage
        /// </summary>
        public static string GetPercent(int Total, int Value)
        {
            var sValue = string.Empty;
            var percent = (double) (Value * 100) / Total;

            //  Console.WriteLine(percent);
            //  Console.WriteLine(Math.Floor(percent));
            //  Console.WriteLine(Math.Ceiling(percent));

            if (Total + Value == 0)
            {
                sValue = "0%";
            }
            else
            {
                sValue = string.Format("{0:0.##}%", percent); //sValue = string.Format("{0.##}%", Math.Floor(percent));
            }

            return sValue;
        }

        public static int IntegerNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return 0;
            }

            if (dbvalue == null)
            {
                return 0;
            }

            return Convert.ToInt32(dbvalue);
        }

        public static string RelativeTime(DateTime dbDateTime)
        {
            var value = string.Empty;

            var currentTime = DateTime.UtcNow;

            var ts = currentTime.Subtract(dbDateTime);

            if (ts.TotalHours >= 24)
            {
                value = $"{dbDateTime:d-MMM-yyyy}";
            }
            else
            {
                if (ts.TotalHours >= 1)
                {
                    value = $"{(int) ts.TotalHours}h ago";
                }
                else
                {
                    value = ts.TotalMinutes < 1 ? "now" : $"{(int) ts.TotalMinutes}m ago";
                }
            }

            return value;
        }

        /// <summary>
        ///     Convert Integer to Hexidecimal
        /// </summary>
        public static string ReturnHex(int intValue)
        {
            var hexValue = intValue.ToString("X");
            return hexValue;
        }

        /// <summary>
        ///     Convert Hexidecimal to Integer
        /// </summary>
        public static int ReturnInteger(string hexValue)
        {
            var intAgain = int.Parse(hexValue, NumberStyles.HexNumber);
            return intAgain;
        }

        public static string StringNull(object dbvalue)
        {
            if (ReferenceEquals(dbvalue, DBNull.Value))
            {
                return"";
            }

            if (dbvalue == null)
            {
                return"";
            }

            return dbvalue.ToString();
        }

        public static string TimeElapsed(DateTime startTime)
        {
            var span = DateTime.UtcNow.Subtract(startTime);

            var sb = new StringBuilder();
           
            if (span.TotalMinutes > 0)
            {
                sb.Append($"{span.Minutes} minutes, ");
            }

            if (span.Seconds > 0)
            {
                sb.Append($"{span.Seconds} seconds, ");
            }

            return $"{sb} {span.Milliseconds} milliseconds.";
        }

        /// <summary>
        ///     Make sure value is not less than 0 and not greater than 2.375 QB Rating
        /// </summary>
        private static double SetMinMax(double value)
        {
            var dValue = value;

            if (value > 2.375)
            {
                dValue = 2.375;
            }
            else if (value < 0)
            {
                dValue = 0.0;
            }

            return dValue;
        }

        /// <summary>
        ///     Check if string is an email address
        /// </summary>
        public static bool IsStringEmailAddress(string value)
        {
            var E = new EmailAddressAttribute();
            return E.IsValid(value);
        }

        public static string RatingLevel(int rating)
        {
            var css = "mediumRating";

            if (rating >= 90)
            {
                css = "highRating";
            }

            if (rating <= 70)
            {
                css = "lowRating";
            }

            return css;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}