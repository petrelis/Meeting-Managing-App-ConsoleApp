using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClass
{
    internal class DateFunctions
    {
        public static DateTimeOffset GetStartDateTime()
        {
            // Enter starting date and time
            while(true)
            {
                Console.WriteLine("Enter starting date in this format: mm/dd/yyyy hh:mm");
                string dateInput = Console.ReadLine();
                if (DateTimeOffset.TryParse(dateInput, out DateTimeOffset startDateOut))
                {
                    return startDateOut;
                }
                Console.WriteLine("You entered an invalid date.");
            }
        }

        public static DateTimeOffset GetEndDateTime(DateTimeOffset startDate)
        {
            Console.Write("How long will the meeting take in minutes: ");
            var length = MiscFunctions.GetIntFromReadLine();
            return startDate.AddMinutes(length);
        }
    }

    internal class FileFunctions
    {
        public static string GetFilePathFromBaseDirectory(string fileName)
        {
            string parentDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            return Path.Combine(parentDirectory, fileName);
        }
    }

    internal class MiscFunctions
    {
        public static int GetIntFromReadLine()
        {
            while(true)
            {
                string input = Console.ReadLine();
                if(int.TryParse(input, out int number))
                {
                    return number;
                }
                Console.WriteLine("You entered an invalid number, try again:");
            }
        }
        public static int GetEnumIndex(int enumCount)
        {
            while (true)
            {
                int index = GetIntFromReadLine();
                if (index <= enumCount && index >= 0)
                {
                    return index;
                }
                Console.Write("You entered an invalid index, try again: ");
            }
        }
        public static string GetNotNullStringFromReadLine(string stringName)
        {
            while(true)
            {
                string input = Console.ReadLine();
                if(input != null && input != String.Empty)
                {
                    return input;
                }
                Console.Write($"Please enter a valid {stringName}: ");
            }
        }
    }
}
