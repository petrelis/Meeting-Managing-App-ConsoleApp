using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public class ConsoleInputOutputFunctions
    {
        public static int GetIntFromReadLine()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
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
            while (true)
            {
                string input = Console.ReadLine();
                if (input != null && input != String.Empty)
                {
                    return input;
                }
                Console.Write($"Please enter a valid {stringName}: ");
            }
        }
    }
}
