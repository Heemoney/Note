namespace Executable
{
    using System;
    using static Note.StringUtils;

#if DEBUG
    /// <summary>
    /// For active debugging class libraries
    /// </summary>
    class DebuggingExecutable
    {
        static void Main()
        {
            string s = null;
            Console.WriteLine(s.ContainsDigits());
        }
    }
#endif
}
