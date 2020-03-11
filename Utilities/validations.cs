using System;

namespace Utilities
{
    public class Validations
    {
        public static (bool, string) ValidateIsNegative(int number)
        {
            if(number <= 0)
            {
                return (false, "Error, number below or equal to 0.");

            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool, string) ValidateForNullOrWhitespace(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return (false, "Error, text is NULL or Whitespace only.");

            }
            else
            {
                return (true, string.Empty);
            }
        }
    }
}
