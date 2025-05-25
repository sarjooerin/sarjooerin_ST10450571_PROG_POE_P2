using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotAI
{
    internal class Utils
    {
        public string Name { get; set; }
        public string MakeTextBold(string text)
        {
            return $"\u001b[1m{text}\u001b[0m";
        }
    }
}