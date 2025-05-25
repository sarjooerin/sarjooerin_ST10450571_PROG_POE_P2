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
                // Sentiment Detection - Negative Emotions
                if (userInput.Contains("sad") || userInput.Contains("upset") || userInput.Contains("angry") || userInput.Contains("frustrated"))
                {
                    if (!string.IsNullOrEmpty(lastTopic))
                    {
                        var topicMatch = library.data
                            .FirstOrDefault(x => x.Subject.Equals(lastTopic, StringComparison.OrdinalIgnoreCase));

                        if (topicMatch != null)
                        {
                            Border.WriteLineColored($"I'm sorry you're feeling that way about {topicMatch.Subject.ToUpper()}.\nIf you'd like, I can share something about cybersecurity to help distract you.", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Border.WriteLineColored("I'm sorry you're feeling that way. Let me share something general about cybersecurity to help distract you.", ConsoleColor.Magenta);
                        }
                    }
                    else
                    {
                        Border.WriteLineColored("I'm sorry you're feeling that way. Ask me something about cybersecurity to help distract you.", ConsoleColor.Magenta);
                    }
                    continue;
                }

                // Sentiment Detection - Positive Emotions
                if (userInput.Contains("happy") || userInput.Contains("excited") || userInput.Contains("glad") || userInput.Contains("great"))
                {
                    if (!string.IsNullOrEmpty(lastTopic))
                    {
                        var topicMatch = library.data
                            .FirstOrDefault(x => x.Subject.Equals(lastTopic, StringComparison.OrdinalIgnoreCase));

                        if (topicMatch != null)
                        {
                            Border.WriteLineColored($"That's great to hear! Glad you're enjoying {topicMatch.Subject.ToUpper()}.\nLet me know what you'd like to learn today.", ConsoleColor.Green);
                        }
                        else
                        {
                            Border.WriteLineColored("That's great to hear! Let me know what you'd like to learn today.", ConsoleColor.Green);
                        }
                    }
                    else
                    {
                        Border.WriteLineColored("That's great to hear! Let me know what you'd like to learn today.", ConsoleColor.Green);
                    }
                    continue;
                }

                // Sentiment Detection - Boredom or Low Energy
                if (userInput.Contains("bored") || userInput.Contains("meh") || userInput.Contains("tired"))
                {
                    if (!string.IsNullOrEmpty(lastTopic))
                    {
                        var topicMatch = library.data
                            .FirstOrDefault(x => x.Subject.Equals(lastTopic, StringComparison.OrdinalIgnoreCase));

                        if (topicMatch != null)
                        {
                            Border.WriteLineColored($"It seems like {topicMatch.Subject.ToUpper()} isn't quite your thing.\nLet's switch it up with another cybersecurity topic!", ConsoleColor.DarkYellow);
                        }
                        else
                        {
                            Border.WriteLineColored("Sounds like you're not into the current topic. Let's spark interest with something new!", ConsoleColor.DarkYellow);
                        }
                    }
                    else
                    {
                        Border.WriteLineColored("Feeling bored? Let's spark some interest with a new cybersecurity topic!", ConsoleColor.DarkYellow);
                    }
                    continue;
                }
                // Conversation Flow - More Info Requests
                if ((userInput.Contains("tell me more") || userInput.Contains("tips")) && !string.IsNullOrEmpty(lastTopic))
                {
                    var topicResponses = library.data.Where(x => x.Subject.Equals(lastTopic, StringComparison.OrdinalIgnoreCase)).ToList();
                    var random = new Random();
                    var response = topicResponses[random.Next(topicResponses.Count)].Content;

                    Border.WriteBorderedMessage($"More about {lastTopic.ToUpper()}:\n\n{response}\n\nIf ready to end session type exit.");
                    continue;
                }

                if ((userInput.Contains("confused") || userInput.Contains("what do you mean")) && !string.IsNullOrEmpty(lastTopic))
                {
                    var topicResponses = library.data.Where(x => x.Subject.Equals(lastTopic, StringComparison.OrdinalIgnoreCase)).ToList();
                    Border.WriteBorderedMessage($"Let me rephrase {lastTopic.ToUpper()}:\n\n{topicResponses.First().Content}\n\nIf ready to end session type exit.");
                    continue;
                }

                // Topic Matching
                var matched = library.data
                    .Where(x => userInput.Contains(x.Subject.ToLower()))
                    .Select(x => x.Subject)
                    .Distinct()
                    .FirstOrDefault();

                if (matched == null)
                {
                    Border.WriteLineColored("🤔 I didn’t quite understand that. Please rephrase or try a different topic.", ConsoleColor.Red);
                }
                else
                {
                    lastTopic = matched;
                    var topicResponses = library.data.Where(x => x.Subject.Equals(matched, StringComparison.OrdinalIgnoreCase)).ToList();
                    var random = new Random();
                    var response = topicResponses[random.Next(topicResponses.Count)].Content;

                    Border.WriteBorderedMessage($"🧠 Topic: {lastTopic.ToUpper()}:\n\n{topicResponses.First().Content}\n\nIf ready to end session type exit.");
                }
            }
        }
    }
}