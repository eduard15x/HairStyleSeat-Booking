using System.Text.RegularExpressions;

namespace backend.Helpers
{
    public class HelperInputValidationRegex
    {
        private static readonly string WorkDaysForSalon = "^(monday|tuesday|wednesday|thursday|friday|saturday|sunday)(,(monday|tuesday|wednesday|thursday|friday|saturday|sunday))*$";
        private static readonly string ValidTimeFormat = "^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";

        public static bool CheckWorkDaysInput(string input)
        {
            return Regex.IsMatch(input, WorkDaysForSalon);
        }

        public static bool CheckValidTimeFormat(string input)
        {
            return Regex.IsMatch(input, ValidTimeFormat);
        }
    }
}
