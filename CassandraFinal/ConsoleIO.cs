using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal {

    public static class ConsoleIO {

        public static string PromptForInput(string prompt, bool canBeEmpty) {
            while (true) {
                string input = null;
                Console.Write(prompt);
                input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input) && !canBeEmpty) {
                    Console.WriteLine("\nYour response cannot be empty. Try again.\n");
                    continue;
                }
                return input;
            }
        }

        public static int PromptForInt(string prompt, string errorMessage, int min = int.MinValue, int max = int.MaxValue) {
            while (true) {
                if (!int.TryParse(PromptForInput(prompt, false), out int input)) {
                    Console.WriteLine($"\n{errorMessage}\n");
                    continue;
                }
                if (input < min || input > max) {
                    Console.WriteLine("\nYour input was out of bounds. Try again.\n");
                    continue;
                }
                return input;
            }
        }

        public static int PromptForMenuSelection(string prompt, object[] options, bool canExit, string exitMessage = "Quit") {
            if (canExit) Console.WriteLine($"0 - {exitMessage}");
            int min = canExit ? 0 : 1;
            for (int i = 1; i <= options.Length; i++) {
                Console.WriteLine($"{i} - {options[i - 1]}");
            }
            return PromptForInt(prompt, "You didn't choose one of the specified options. Try again.", min, options.Length);
        }
    }
}
