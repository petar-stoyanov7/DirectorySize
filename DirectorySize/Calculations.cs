using System;

namespace DirectorySize
{
    public class Calculations
    {        
        
        public static double ToKilobytes(long value)
        {
            float result =  (float)value / 1024;
            return Math.Round(result, 2);
        }

        public static double ToMegabytes(long value)
        {
            float result = (float)value / 1048576;
            return Math.Round(result, 2);

        }

        public static double ToGigabytes(long value)
        {
            float result = (float)value / 1073741824;
            return Math.Round(result, 2);
        }

        public static string HumanReadable(long value)
        {
            if (value <= 1024)
            {
                return Convert.ToString(value) + " bytes";
            }
            else if (value > 1024 && value <= 1048576)
            {
                var result = Calculations.ToKilobytes(value);
                return Convert.ToString(result) + " KB";
            }
            else if (value > 1048576 && value <= 1073741824)
            {
                var result = Calculations.ToMegabytes(value);
                return Convert.ToString(result) + " MB";
            }
            else if (value > 1073741824)
            {
                var result = Calculations.ToGigabytes(value);
                return Convert.ToString(result) + " GB";
            }
            else
                return "";
        }
    }    
}
