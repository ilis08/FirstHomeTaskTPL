using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace WordAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            TextManager manager = new TextManager();

            Console.WriteLine("Start of non-parallel execution");

            Stopwatch watch1 = new Stopwatch();

            watch1.Start();
            GetNumberOfWords(manager.Text);
            ShortestWord(manager.Text);
            LongestWord(manager.Text);
            AverageWordLength(manager.Text);
            FiveMostCommon(manager.Text);
            FiveLeastCommon(manager.Text);
            watch1.Stop();

            Console.WriteLine($"The time of non-parallel execution is {watch1.ElapsedMilliseconds}");

            Console.WriteLine("-------------------------------------------------------------------");

            Console.WriteLine("Start of a parallel execution");

            List<Thread> threads = new List<Thread>()
            {
                new Thread(() => {GetNumberOfWords(manager.Text); }),
                new Thread(() => {ShortestWord(manager.Text); }),
                new Thread(() => {LongestWord(manager.Text); }),
                new Thread(() => {AverageWordLength(manager.Text); }),
                new Thread(() => {FiveMostCommon(manager.Text); }),
                new Thread(() => {FiveLeastCommon(manager.Text); }),
            };

            Stopwatch watch2 = new Stopwatch();

            watch2.Start();
            foreach (var item in threads)
            {
                item.Start();
            }
            watch2.Stop();

            foreach (var item in threads)
            {
                item.Join();
            }

            Console.WriteLine($"The time of paralel execution is {watch2.ElapsedMilliseconds}");
        }

        static void GetNumberOfWords(string[] text)
        {
            int number = text.Count();

            Console.WriteLine($"Number of words in text is {number}.");
        }

        static void ShortestWord(string[] text)
        {
            var word = text.OrderBy(c => c.Length).FirstOrDefault();

            Console.WriteLine($"The shortest word is {word}.");
        }

        static void LongestWord(string[] text)
        {
            var word = text.OrderByDescending(c => c.Length).FirstOrDefault();

            Console.WriteLine($"The longest word is {word}.");
        }

        static void AverageWordLength(string[] text)
        {
            var averageWord = text.Average(x => x.Length);

            Console.WriteLine($"Average word length is {averageWord}.");
        }

        static void FiveMostCommon(string[] text)
        {
            var query = text.GroupBy(x => x).Select(x => new { KeyField = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).Take(5);

            IEnumerable<string> list = query.ToList().Select(c => c.KeyField);

            string words = String.Join(',', list);

            Console.WriteLine($"The five most popular words is : {words}.");
        }

        static void FiveLeastCommon(string[] text)
        {
            var query = text.GroupBy(x => x).Select(x => new { KeyField = x.Key, Count = x.Count() }).OrderBy(x => x.Count).Take(5);

            IEnumerable<string> list = query.ToList().Select(c => c.KeyField);

            string words = String.Join(',', list);

            Console.WriteLine($"The five least popular words is : {words}.");
        }
    }
}
