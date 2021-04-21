using System;
using System.Threading;

namespace Client
{
    public class MenuClient
    {
        public int ShowMenu( string[] _options,bool salir)
        {
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix =i;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{prefix}{_options[i]}");
                }

                string var = "0";
                var thread = new Thread(x => var = Console.ReadLine());
                thread.Start();
                int option= Int32.Parse(var);
                return option;
        }
        
    }
}
