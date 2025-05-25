using System;
using System.Drawing;
using System.IO;

internal class ImageDisplay
{
    public void Show(string filename = "image.jpg")
    {
        try
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", filename);
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

                int consoleWidth = Console.WindowWidth;
                int padding = Math.Max((consoleWidth - newWidth), 0) / 2;
                string pad = new string(' ', padding);

                for (int y = 0; y < resized.Height; y++)
                {
                    Console.Write(pad);
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
