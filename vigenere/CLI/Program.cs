using System;
using Vigenere.Core;

namespace Vigenere.CLI
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*
             * Użycie:
             * CLI.exe [polecenie] [nazwa_pliku] [klucz]
             * polecenie: "szyfruj" lub "deszyfruj"
             * nazwa_pliku: nazwa pliku do zaszyfrowania
             * klucz: oddzielone spacją liczby oznaczające przesunięcie liter
             * 
             * Program działa w pierścieniu Z 26.
             */
            try
            {
                var parser = new Parser(args);
                parser.Parse();
                Console.WriteLine(parser.OutputText);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
