using System;
using Note.Strings;
using Note.File;
using Note.MathUtils;

namespace Note.Executable
{
#if DEBUG
    /// <summary>
    /// For active debugging class libraries
    /// </summary>
    class DebuggingExecutable
    {
        static void Main()
        {
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Manu\source\repos\Note\Executable\TextFile1.txt");
            string s = "manu is very cool and he is a cool person";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string change = s.Shuffle(true);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
            Console.WriteLine(change);

        }
    }
#endif
}
