using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNativeCode
{
     class Win32Lib
    {
        // Use DllImport to import the Win32 MessageBox function.
        [DllImport("user32.dll", EntryPoint = "MessageBox")]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);


        public static void Start()
        {
            OpenMessageBox();
        }

        private static void OpenMessageBox()
        {
            MessageBox(new IntPtr(0), "Hello World via calling Win32 MessageBox!", "Win32 Dialog", 0);
        }
    }
}
