using System;

namespace Vending
{
    public class PreparationLog : IDisposable
    {
        private readonly string _title;
        public PreparationLog(string title)
        {
            _title = title;
            Console.WriteLine($"[+] журнал открыт: {_title}");
        }
        public static void Write(string line)
        {
            Console.WriteLine($"  {line}");
        }
        public void Dispose()
        {
            Console.WriteLine($"[-] журнал закрыт: {_title}");
        }
    }
}