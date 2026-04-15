using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter a number");
            string input = Console.ReadLine();
            int number = Convert.ToInt32(input);
            if(number<0)
            {
                Console.WriteLine("Negative number entered Please Enter a positivr number");    
                continue;
            }
            else
            {
                long factorial = CalculateFactorial(number);
                Console.WriteLine($"Factorial of {number} is {factorial}");
            }
        }
    }
    static long CalculateFactorial(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        else
            return n * CalculateFactorial(n - 1);
    }
}