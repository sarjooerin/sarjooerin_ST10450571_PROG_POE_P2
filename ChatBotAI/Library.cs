using System;
using System.Collections.Generic;
using System.IO;

namespace ChatBotAI
{
    internal class Library
    {
        public List<ChatBotData> data = new List<ChatBotData>();

        public void LoadData()
        {
            // Load ChatBotData.txt
            if (File.Exists("Assets/ChatBotData.txt"))
            {
                string[] lines = File.ReadAllLines("Assets/ChatBotData.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        data.Add(new ChatBotData
                        {
                            Subject = parts[0].Trim(),
                            Content = parts[1].Trim()
                        });
                    }
                }
            }
        }
    }
}
