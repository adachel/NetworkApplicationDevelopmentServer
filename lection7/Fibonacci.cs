namespace lection7
{
    public class Fibonacci
    {
        public int CalculateLec7(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("Fibonacci number can`t less then 1");

            if (number > 46)
                throw new ArgumentOutOfRangeException("Fibonacci exceed max integer value");

            int first = 1;
            int second = 1;
            for (int i = 2; i <= number; i++)
            {
                int temp = first;
                first = second;
                second = first + temp;
            }
            return first;
        }

        public int CalculateRecLec7(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("Fibonacci number can`t less then 1");

            if (number > 46)
                throw new ArgumentOutOfRangeException("Fibonacci exceed max integer value");

            if (number == 1) return 1;

            if (number == 2) return 1;

            return CalculateRecLec7(number - 1) + CalculateRecLec7(number - 2);
        }


    }
}
