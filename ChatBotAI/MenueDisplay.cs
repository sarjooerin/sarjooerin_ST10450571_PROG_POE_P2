using System;
using System.Collections.Generic;
using System.Linq;


namespace ChatBotAI
{
    internal class MenuDisplay
    {
        private static string lastTopic = null;
        private static Dictionary<string, string> memory = new Dictionary<string, string>();



        public static void LoadMenu(string personName)
        {
            Library library = new Library();
            library.LoadData();

            Console.Clear();
            Border.WriteBorderedMessage($"👋 Welcome {personName}! Here are some topics you can learn about:");

            foreach (var item in library.data.Select(d => d.Subject).Distinct())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"🔹 {item}");
            }

            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                Border.WritePrompt("\nWhat would you like to learn about:\n> ");
                string userInput = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Border.WriteLineColored("🤔 I didn't quite catch that. Can you try again?", ConsoleColor.DarkYellow);
                    continue;
                }

                if (userInput == "exit")
                {
                    Border.WriteBorderedMessage($"👋 Goodbye! Stay cyber safe, {personName}!");
                    break;
                }

                // Memory and Recall
                if (userInput.StartsWith("remember "))
                {
                    string value = userInput.Replace("remember that i like ", "").Trim();
                    memory["favorite"] = value;
                    Border.WriteLineColored($"Got it! I’ll remember that you like {value}.", ConsoleColor.Green);
                    continue;
                }

                if (userInput == "what do you remember")
                {
                    if (memory.Count == 0)
                    {
                        Border.WriteLineColored("🤔 I don’t remember anything yet.", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        foreach (var kvp in memory)
                        {
                            Border.WriteLineColored($"{kvp.Key.ToUpper()}: {kvp.Value}", ConsoleColor.Cyan);
                        }
                    }
                    continue;
                }

                if (userInput == "forget everything")
                {
                    memory.Clear();
                    Border.WriteLineColored("Memory cleared!", ConsoleColor.Magenta);
                    continue;
                }
            }
        }
    }
}
