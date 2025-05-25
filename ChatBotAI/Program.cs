using ChatBotAI;
using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Utils utils = new Utils();
        TypingEffect(utils.MakeTextBold("HI:) I'M CHATBOT A CYBERSECURITY AWARENESS HELPER!"));

        AudioPlayer audioPlayer = new AudioPlayer();
        audioPlayer.Play();

        ImageDisplay display = new ImageDisplay();
        display.Show();

        Greeting greeting = new Greeting();
        greeting.Greet();

        Console.ReadKey();
    }



    static void TypingEffect(string message)
    {
        foreach (char letter in message)
        {
            Console.Write(letter);
            Thread.Sleep(40); 
        }
    }
}
