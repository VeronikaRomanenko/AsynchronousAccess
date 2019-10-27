using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = { "../../Program.cs", "../../ConsoleApp6.csproj", "../../Properties/AssemblyInfo.cs" };
            AsyncReader[] asrArr = new AsyncReader[3];
            for (int i = 0; i < asrArr.Length; i++)
            {
                asrArr[i] = new AsyncReader(new FileStream(files[i], FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous), 100);
            }
            Console.ReadLine();
        }
    }

    class AsyncReader
    {
        FileStream stream;
        byte[] data;
        public AsyncReader(FileStream s, int size)
        {
            stream = s;
            data = new byte[size];
            stream.BeginRead(data, 0, size, delegate(IAsyncResult asRes) { stream.EndRead(asRes); stream.Close(); Console.WriteLine($"Файл:{stream.Name}\n{Encoding.UTF8.GetString(data)}\n\n"); }, null);
        }
    }
}