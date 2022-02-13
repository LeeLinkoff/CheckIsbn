using System;


namespace ISBN
{
    public class ISBN
    {
        public static Boolean checkISBN(String value)
        {
            Boolean validIsbn = false;

            // NOTE:
            // * ISBN-10 uses mod 11, so check digit can be 0 thru 9 or X (representing 10) - 10 "digits"
            // * ISBN-13 uses mod 10, so check digit can only be 0 thru 9

            // 1st, remove any hyphens or spaces
            value = value.Replace("-", "").Replace(" ", "");

            // 2nd, see if string is anything other than 9 digits + 'X', 10 digits or 13 digits,
            // if it is, not a valid ISBN-10 or ISBN-13
            long num;
            Boolean thirteenDigits = (value.Length == 13) && (long.TryParse(value,out num));
            Boolean tenDigits = (value.Length == 10) && (long.TryParse(value, out num));
            Boolean nineDigitsAndX = (value.Length == 10) && (long.TryParse(value.Substring(0,9), out num)) && (value.Substring(9).ToUpper() == "X");
            
            // finally, check if valid ISBN-10 or ISBN-13, by checking the check digit
            if (thirteenDigits || tenDigits || nineDigitsAndX)
            {
                int prod;
                int sum = 0;

                int calculatedCheckDigit;

                if (value.Length == 10)
                {
                    
                    // multiply each digit, from left to right, by it's position, excluding check digit
                    // (example: 1st digit X 1, 2nd digit X 2 ... 9th digit X 9)
                    // those products are summed
                    for (int i = 1; i < 10; i++)
                    {
                        prod = i * (value[i-1] - '0');  //product of each digit by its position - account for 0 based indexing in C#
                        sum += prod;                    //summing products
                    }

                    // take modulo 11 of the summed products
                    calculatedCheckDigit = sum % 11;

                    // if the "calculated" check digit equals the actual check digit, then it is a valid ISBN-10
                    if (
                         ((calculatedCheckDigit == 10) && (value[9].ToString().ToUpper() == "X")) 
                          ||
                         (calculatedCheckDigit == (value[9] - '0'))
                       )
                    {
                        validIsbn = true;
                    }
                }
                else if (value.Length == 13)
                {
                    prod = 0;
                    sum = 0;

                    int digitValue;

                    // multiply each digit, from left to right, alternatively by 1 or 3
                    // (example: 1st digit X 1, 2nd digit X 3, 3rd digit X 1, 4th digit X 3 , 5th digit X 1, 6th digit X 3...)
                    // those products are summed
                    for (int i = 1; i < 13; i++)
                    {
                        digitValue = (value[i - 1] - '0');  //account for 0 based indexing in C#

                        // another way of stating above is:
                        // digits in odd positions are multiple by 1,
                        // digits in even postions are multiplied by 3
                        prod = digitValue * (i % 2 == 1 ? 1 : 3);  //product of each digit by alernating 1 (odd postions) and 3 (even postions)
                        sum += prod;                               //summing products
                    }

                    // take modulo 10 of result and subtracting this value from 10, then taking the modulo of the result again
                    calculatedCheckDigit = (10 - (sum % 10)) % 10;

                    // if the "calculated" check digit equals the actual check digit, then it is a valid ISBN-10
                    if (calculatedCheckDigit == (value[0] - '0'))
                    {
                        validIsbn = true;
                    }
                }
            }

            
            return validIsbn;
        }

        public static Boolean checkIsbn10(string value)
        {
            Boolean validIsbn = false;

            int prod;
            int sum = 0;

            int remainder;

            // 1st, remove any hyphens or spaces
            value = value.Replace("-", "").Replace(" ", "");

            // NOTE:
            // * ISBN-10 uses mod 11, so check digit can be 0 thru 9 or X (representing 10) - 10 "digits"
            //
            // 2nd, see if string is anything other than 9 digits + 'X' or 10 digits
            // if it is, not a valid ISBN-10 
            long num;
            Boolean tenDigits = (value.Length == 10) && (long.TryParse(value, out num));
            Boolean nineDigitsAndX = (value.Length == 10) && (long.TryParse(value.Substring(0, 9), out num)) && (value.Substring(9).ToUpper() == "X");

            // finally, check if valid ISBN-10, by calculating weighted sum and verifying it is a multiple of 11
            if (tenDigits || nineDigitsAndX)
            {
                // multiply each digit, from left to right, by it's position starting from 10 counting down to 1
                // (example: 1st digit X 10, 2nd digit X 9 ... 10th digit X 1)
                // those products are summed
                for (int i = 0; i < 9; i++)
                {
                    prod = (10 - i) * (value[i] - '0');      //product of each digit by its position - account for 0 based indexing in C#
                    sum += prod;                             //summing products
                }

                // if last digit (from left to right, righmost 'digit') is 'X', add 10 to sum,
                // otherwise add it's value
                if (nineDigitsAndX)
                {
                    sum += 10;
                }
                else
                {
                    sum += (value[9] - '0');
                }

                // get remainder of sum divided by 11, take modulo 11 of the summed products
                remainder = sum % 11;

                // if the "calculated" check digit equals the actual check digit, then it is a valid ISBN-10
                if (remainder == 0)
                {
                    validIsbn = true;
                }
            }

            return validIsbn;
        }
    }
}
