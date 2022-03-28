using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallNativeCode
{
    class RuntimeLoadDll
    {

        public static void Start()
        {
            Console.WriteLine("Please input dll path:");

            var dllPath = Console.ReadLine();
            if (dllPath is null || dllPath.Length == 0)
            {
                dllPath = "C:\\Users\\xxx\\source\\repos\\Docs.Localization.Build\\src\\shared\\bin\\Debug\\net6.0\\shared.dll";
            }

            try
            {
                Console.WriteLine("Load dll from : " + dllPath);
                var mathDll = Assembly.LoadFile(dllPath);
                Type[] types = mathDll.GetExportedTypes();
                foreach (var t in types)
                {
                    //Console.WriteLine(t.Name);
                    if (t.Name == "PathUtility")
                    {
                        object[] parameters = new object[] { "c://a//b\\c\\d" };
                        var result = t.GetMethod("Normalize").Invoke(null, parameters);
                        Console.WriteLine("Using PathUtility.Normalize to output normalize Path: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
