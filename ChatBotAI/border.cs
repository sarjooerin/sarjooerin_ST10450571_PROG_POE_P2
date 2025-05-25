internal static class Border
{
    private const int BorderWidth = 60;

    public static void WriteBorderedMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        string top = "╔" + new string('═', BorderWidth) + "╗";
        string bottom = "╚" + new string('═', BorderWidth) + "╝";

        Console.WriteLine(top);
        foreach (var line in WrapText(message, BorderWidth))
        {
            Console.WriteLine($"║{line.PadRight(BorderWidth)}║");
        }
        Console.WriteLine(bottom);
        Console.ResetColor();
    }

    public static void WritePrompt(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(prompt);
        Console.ResetColor();
    }

    public static void WriteLineColored(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static List<string> WrapText(string text, int maxLineLength)
    {
        List<string> wrappedLines = new List<string>();
        string[] words = text.Split(' ');

        string line = "";
        foreach (var word in words)
        {
            if ((line + word).Length > maxLineLength)
            {
                wrappedLines.Add(line.TrimEnd());
                line = "";
            }
            line += word + " ";
        }

        if (!string.IsNullOrEmpty(line))
        {
            wrappedLines.Add(line.TrimEnd());
        }

        return wrappedLines;
    }
}
