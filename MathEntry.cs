using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static CallNativeCode.MathLibrary;

namespace CallNativeCode
{
    public class MathEntry
    {

        public static void Start()
        {
            Console.WriteLine("Fibonacci numbers");

            FibonacciInit(0L, 1L);
            string input;
            while (Console.ReadLine() != "q")
            {
                if (FibonacciNext())
                {
                    Console.WriteLine($"#{FibonacciIndex()}: {FibonacciCurrent()}");
                }
            }
            Console.WriteLine("Bye~");
        }
    }
}
