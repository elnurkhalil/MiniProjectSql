using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace CompanyApp.ConsoleApp.Controllers
{
    public static class ConsoleHelper
    {
        public static string ReadString(string prompt, bool allowNumbers = true, bool allowPunctuation = true, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (allowEmpty) return string.Empty;
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }

                if (!allowNumbers && input.Any(char.IsDigit))
                {
                    Console.WriteLine("Input must not contain numbers.");
                    continue;
                }

                if (!allowPunctuation && input.Any(char.IsPunctuation))
                {
                    Console.WriteLine("Input must not contain punctuation marks.");
                    continue;
                }

                return input;
            }
        }

        public static int ReadNonNegativeInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out var v) && v >= 0)
                    return v;

                Console.WriteLine("Please enter a valid non-negative integer (0 or greater).");
            }
        }

        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out var v) && v > 0)
                    return v;

                Console.WriteLine("Please enter a valid positive integer (greater than 0).");
            }
        }
    }
}
