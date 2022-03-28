using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNativeCode
{
    internal class MathLibrary
    {
        private const string LibName = "MathDll";

        [DllImport(LibName, EntryPoint = "fibonacci_init")]
        public static extern unsafe void FibonacciInit(long a, long b);

        [DllImport(LibName, EntryPoint = "fibonacci_next")]
        public static extern unsafe bool FibonacciNext();


        [DllImport(LibName, EntryPoint = "fibonacci_current")]
        public static extern unsafe long FibonacciCurrent();


        [DllImport(LibName, EntryPoint = "fibonacci_index")]
        public static extern unsafe int FibonacciIndex();
    }
}
