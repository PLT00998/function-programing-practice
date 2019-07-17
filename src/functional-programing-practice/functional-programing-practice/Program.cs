using functional_programing_practice.Chapters;
using System;

namespace functional_programing_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            IChapter chapter = new Chapter1();
            chapter.Experiment();
            Console.Read();
        }
    }
}
