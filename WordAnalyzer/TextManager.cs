using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAnalyzer
{
    public class TextManager
    {
        public TextManager()
        {
            GetText();
        }

        private string path = @"C:\IzbiraemaParallelProgrammingC#\Hometasks\FirstHomeTask\WordAnalyzer\Text";

        public string [] Text { get; set; }

        private string text { get; set; }

        public void GetText()
        {
            using (FileStream fstream = new FileStream(@$"{path}\WarPeace.txt", FileMode.Open)) 
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                text = Encoding.Default.GetString(array);

                Text = text.Split(new char[]
                {
                     ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/', '\r', '!', '"'
                },
                StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < Text.Length; i++)
                {
                    Text[i] = Text[i].ToLower();
                }
            }
        }
    }
}
