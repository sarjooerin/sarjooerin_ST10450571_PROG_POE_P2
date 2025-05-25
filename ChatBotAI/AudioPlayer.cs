using System;
using System.Media;
using System.IO;

namespace ChatBotAI
{
    internal class AudioPlayer
    {
        public void Play()
        {
            try
            {
               
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");

                
                if (!File.Exists(filePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Audio file not found: " + filePath);
                    Console.ResetColor();
                    return;
                }

                
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync(); 
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("there was an error playing audio: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
