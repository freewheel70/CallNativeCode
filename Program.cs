using CallNativeCode;

PrintOptions();

string option;
while ((option = Console.ReadLine()) != "q")
{
    switch (option)
    {
        case "1":
            MathEntry.Start();
            break;
        case "2":
            GitEntry.Start();
            break;
        case "3":
            Win32Lib.Start();
            break;
        case "4":
            RuntimeLoadDll.Start();
            break;
        default:
            break;

    }

    PrintOptions();
}
Console.WriteLine("Exit Demo");

static void PrintOptions()
{
    Console.WriteLine("Demos: \n1. Math Library\n2. Git Lib\n3. Call Win32 Lib\n4. Dynamic libraries");
}