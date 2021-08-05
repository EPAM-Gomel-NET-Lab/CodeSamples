using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureStringDemo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var ss = new SecureString())
            {
                Console.Write("Please enter password: ");
                while (true)
                {
                    var cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter) break;

                    // Присоединить символы пароля в конец SecureString
                    ss.AppendChar(cki.KeyChar);
                    Console.Write("*");
                }

                Console.WriteLine();
                // Пароль введен, отобразим его для демонстрационных целей     
                DisplaySecureString(ss);
            }
        }

        private static unsafe void DisplaySecureString(SecureString ss)
        {
            char* pc = null;
            try
            {
                pc = (char*) Marshal.SecureStringToCoTaskMemUnicode(ss);

                // Доступ к буферу неуправляемой памяти,
                // который хранит дешифрованную версию SecureString
                if (pc == null)
                {
                    return;
                }

                for (var index = 0; pc[index] != 0; index++)
                {
                    Console.Write(pc[index]);
                }
            }
            finally
            {
                // Обеспечиваем обнуление и освобождение буфера неуправляемой памяти который хранит расшифрованные символы SecureString
                if (pc != null)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
                }
            }
        }
    }
}
