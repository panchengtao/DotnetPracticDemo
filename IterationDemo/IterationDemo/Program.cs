using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterationDemo
{
    class Program
    {
        static readonly String Padding = new String(' ', 30);
        static IEnumerable<Int32> CreateEnumerable()
        {
            Console.WriteLine("{0} CreateEnumerable()方法开始", Padding);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("{0}开始 yield", i);
                yield return i;
                Console.WriteLine("{0}yield 结束", Padding);
            }
            Console.WriteLine("{0} Yielding最后一个值", Padding);
            yield return -1;
            Console.WriteLine("{0} CreateEnumerable()方法结束", Padding);
        }

        static void Main(string[] args)
        {
            IEnumerable<Int32> iterable = CreateEnumerable();
            IEnumerator<Int32> iterator = iterable.GetEnumerator();
            Console.WriteLine("开始迭代");
            while (true)
            {
                Console.WriteLine("调用MoveNext方法……");
                Boolean result = iterator.MoveNext();
                Console.WriteLine("MoveNext方法返回的{0}", result);
                if (!result)
                {
                    break;
                }
                Console.WriteLine("获取当前值……");
                Console.WriteLine("获取到的当前值为{0}", iterator.Current);
            }

            Console.ReadKey();
        }
    }
}
