using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotAI
{
    internal class Greeting
    {
        public string Name { get; set; }
        public bool UseGreeted { get; set; }

        public void Greet()
        {
            Border.WriteBorderedMessage("🔒 Welcome to the Cybersecurity Awareness Chat Room! 🔒");

            TypingEffect("Hi, I am CyberBot, what is your name?");
            Border.WritePrompt("");
            Name = Console.ReadLine();
            UseGreeted = true;

            string message = $"Nice to meet you {Name}. How can I be of assistance today";
            TypingEffect(message);

            string messages = $"Here are a few thing you are welcome to ask me about";
            TypingEffect(messages);

            MenuDisplay.LoadMenu(Name);
        }

            private void TypingEffect(string message)
        {
            foreach (char letter in message)
            {
                Console.Write(letter);
                System.Threading.Thread.Sleep(40); 
            }
            Console.WriteLine();
        }

}

    internal class ChatBotData
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    internal class ImageDisplay
    {
        public void Show()
        {
            try
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "image.jpg");
                int width = 60;
                int height = 30;

                if (!File.Exists(imagePath))
                {
                    Border.WriteLineColored($"Image not found: {imagePath}", ConsoleColor.Red);
                    return;
                }

                Console.OutputEncoding = System.Text.Encoding.UTF8;
                ConvertImageToAscii(imagePath, width, height);
            }
            catch (Exception ex)
            {
                Border.WriteLineColored($"Error displaying image: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void ConvertImageToAscii(string imagePath, int newWidth, int newHeight)
        {
            using (Bitmap image = new Bitmap(imagePath))
            {
                using (Bitmap resized = new Bitmap(image, new Size(newWidth, newHeight)))
                {
                    string asciiChars = "@%#*+=-:. ";

                    for (int y = 0; y < resized.Height; y++)
                    {
                        for (int x = 0; x < resized.Width; x++)
                        {
                            Color pixelColor = resized.GetPixel(x, y);
                            int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                            int index = gray * (asciiChars.Length - 1) / 255;
                            Console.Write(asciiChars[index]);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}