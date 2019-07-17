using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace functional_programing_practice.Chapters
{
    public class Chapter1 : IChapter
    {
        public void Experiment()
        {
            //WhereAndOrderDoesnotImpactOriginalList.Try();
            AdaptFunction_SwapArgs.Try();
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

        internal class AdaptFunction_SwapArgs
        {
            public static void Try()
            {
                Func<int, int, int> divide = (x, y) => x / y; //相除委托
                Console.WriteLine($"divide(正常使用):{divide(10, 2)}"); //应该等于5
                Console.WriteLine("如果委托消费方的习惯是[除数(y)在前]，那么在不修改devide的情况下可增加一个[适配器函数]");


                /* 不优雅的一种
                 Result SwapArgs<Y,X,Result>(Func<X,Y,Result> originalFunc, Y y, X x)
                {
                    return originalFunc(x, y);
                }

                int r=SwapArgs(divide, 10, 2);
                 */

                var divideBy = divide.SwapArgs();
                Console.WriteLine($"divideBy改变参数顺序:{ divideBy(2, 10)}");
            }
        }

        internal class DbLogger
        {
            public void Log(string message)
            => Chapter1Extensions.Connect("localhost;uid", c => c.CreateCommand().ExecuteNonQuery());
        }
    }

    public static class Chapter1Extensions
    {
        public static Func<T2, T1, Result> SwapArgs<T1, T2, Result>(this Func<T1, T2, Result> originalFunc)
        //=> (y, x) => default(Result);
        => (y, x) => originalFunc(x, y);
        //{
        //    Result R(T2 t2, T1 t1) => default(Result);
        //    return R;
        //}

        public static R Connect<R>(string connStr, Func<IDbConnection, R> f)
            => Using(new SqlConnection(connStr), conn => { conn.Open(); return f(conn); });

        public static R Using<TDisp, R>(TDisp disposable, Func<TDisp, R> f) where TDisp : IDisposable
        {
            using (var disp = disposable)
            {
                return f(disp);
            }
        }
    }
}
