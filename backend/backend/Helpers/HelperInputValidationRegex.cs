using System.Text.RegularExpressions;

namespace backend.Helpers
{
    public class HelperInputValidationRegex
    {
        private readonly string WorkDaysForSalon = "^(monday|tuesday|wednesday|thursday|friday|saturday|sunday)(,(monday|tuesday|wednesday|thursday|friday|saturday|sunday))*$";

        protected bool CheckWorkDaysInput(string input)
        {
            return Regex.IsMatch(input, WorkDaysForSalon);
        }
    }
}
