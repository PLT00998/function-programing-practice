using System;
using System.Linq;

namespace functional_programing_practice.Chapters
{
    public class Chapter1 : IChapter
    {
        public void Experiment()
        {
            WhereAndOrderDoesnotImpactOriginalList.Try();
        }

        internal class WhereAndOrderDoesnotImpactOriginalList
        {
            public static void Try()
            {
                Func<int, bool> isOdd = x => x % 2 == 1;

                int[] original = { 7, 6, 1 };
                var sorted = original.OrderBy(isOdd);
                var filtered = original.Where(isOdd);

                Console.WriteLine($"original(原始列表不会状态突变当使用函数式范式):{string.Join(",", original)}");
                Console.WriteLine($"sorted:{string.Join(",", sorted)}");
                Console.WriteLine($"filtered:{string.Join(",", filtered)}");
            }
        }
    }
}
