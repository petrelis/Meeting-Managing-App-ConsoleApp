using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public class DateFunctions
    {
        public static DateTimeOffset GetStartDateTime()
        {
            // Enter starting date and time
            while (true)
            {
                string format = "MM/dd/yyyy HH:mm";
                Console.WriteLine("Enter starting date in this format: mm/dd/yyyy hh:mm");
                string dateInput = Console.ReadLine();
                if (DateTimeOffset.TryParseExact(dateInput, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset startDateOut))
                {
                    return startDateOut;
                }
                Console.WriteLine("The date you entered does not follow the format.");
            }
        }

        public static DateTimeOffset GetEndDateTime(DateTimeOffset startDate)
        {
            Console.Write("How long will the meeting take in minutes: ");
            var length = ConsoleInputOutputFunctions.GetIntFromReadLine();
            return startDate.AddMinutes(length);
        }
    }
}
