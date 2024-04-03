using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.HomeWorks.HomeWork2
{
    internal class HW2
    {
        static async Task<int> Method1()
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
                count++;
                Console.WriteLine("Method1 - " + count);
                Thread.Sleep(1000);
                Console.WriteLine($"Method1 Thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            // throw new Exception();
            return count;
        }
        static async Task<int> Method2()
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000); 
                count++;
                Console.WriteLine("Method2 - " + count);
                Console.WriteLine($"Method2 Thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            return count;
        }



        public async Task Run()
        {
            Console.WriteLine($"Main start: {Thread.CurrentThread.ManagedThreadId}");
            var task = Method1();


            var result2 = await Method2();
            Console.WriteLine("Method2 End");
            await Task.Delay(5000);
            Console.WriteLine($"Main end: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
