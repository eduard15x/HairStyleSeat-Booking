using System.Globalization;

namespace backend.Helpers
{
    public class HelperTimeValidation
    {
        public static bool CheckHourWithinInterval(string selectedHour, string startTime, string endTime)
        {
            DateTime userTime = DateTime.ParseExact(selectedHour, "HH:mm", CultureInfo.InvariantCulture);
            DateTime startTimeObj = DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime endTimeObj = DateTime.ParseExact(endTime, "HH:mm", CultureInfo.InvariantCulture);

            if (userTime.TimeOfDay >= startTimeObj.TimeOfDay && userTime.TimeOfDay <= endTimeObj.TimeOfDay)
            {
                return true;
            }
            else
            {
                return false;
            }
            //Note that the "HH:mm" format string specifies that the input will be in 24-hour format with a leading zero for single-digit hours.
            //If your input uses a different format, you'll need to adjust the format string accordingly.
        }
    }
}
