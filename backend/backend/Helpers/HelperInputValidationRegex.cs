using System.Text.RegularExpressions;

namespace backend.Helpers
{
    public class HelperInputValidationRegex
    {
        private readonly string WorkDaysForSalon = "^(monday|tuesday|wednesday|thursday|friday|saturday|sunday)(,(monday|tuesday|wednesday|thursday|friday|saturday|sunday))*$";
        private readonly string ValidTimeFormat = "^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";

        protected bool CheckWorkDaysInput(string input)
        {
            return Regex.IsMatch(input, WorkDaysForSalon);
        }

        protected bool CheckValidTimeFormat(string input)
        {
            return Regex.IsMatch(input, ValidTimeFormat);
        }
    }
}
