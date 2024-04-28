using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection6
{
    // Пример тестирования 
    public class Fibonacci
    {
        public int Calculate(int number)
        {
            int first = 1;
            int second = 1;
            for (int i = 2; i <= number; i++)
            {
                int temp = first;
                first = second;
                second = temp;
            }
            return first;
        }
    }
}
