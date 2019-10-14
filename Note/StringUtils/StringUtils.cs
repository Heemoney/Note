using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Note.Attributes;
using System.Diagnostics.Contracts;

namespace Note
{
    [Author("Manu Puduvalli")]
    public static class StringUtils
    {

        public const char SPACE = (char)32;

        /// <summary>
        /// Reverses a string from left to right order while maintaining case sensitivity.
        /// </summary>
        /// <param name="str">The string to be reversed</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The reversed string</returns>
        public static string Reverse(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return str;
            }

            var c = str.ToCharArray();
            Array.Reverse(c);
            return new string(value: c);
        }

        /// <summary>
        /// Creates a string from the first character of the string to the first whitespace.
        /// </summary>
        /// <param name="str">The string to be chomped</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string retaining the first word</returns>
        public static string Chomp(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (str.Length == 0)
            {
                return str;
            }

            int idx = str.IndexOf(string.Empty);
            if(idx >= 0)
            {
                return str.Substring(0, idx);
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Creates a string from the first character of the string to the nth whitespace that is specified.
        /// </summary>
        /// <param name="str">The string to be chomped</param>
        /// <param name="spaces">The amount of white space to chomp after</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string retaining the chomped word</returns>
        public static string ChompAfter(this string str, int spaces)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return str;
            }
            if (spaces != 0)
            {
                var matching_num_spaces = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsWhiteSpace(str[i]))
                    {
                        int index_of_space = i;
                        matching_num_spaces++;

                        if (spaces == matching_num_spaces)
                        {
                            return str.Substring(0, index_of_space);
                        }
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// Counts the number of words in a string
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The number of words in the string</returns>
        public static int CountWords(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return 0;
            }
            if(str.Length == 1)
            {
                return 1;
            }
            return str.Split().Length;
        }

        /// <summary>
        /// Removes all instances of any number of characters from a specified string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAll(this string str, params IEnumerable<char>[] args)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            if (args == null) throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            var sb = new StringBuilder(str);
            for (int i = 0; i < args.Length; i++)
            {
                sb.Replace(args[i].ToString(), string.Empty);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all instances of any number of strings from a specified string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAll(this string str, params IEnumerable<string>[] args)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            if (args == null) throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            var sb = new StringBuilder(str);

            for (int i = 0; i < str.Length; i++)
            {
                int cnt = args[i].Count();
                var i_str = args[i].ToString();
                if(cnt == 1)
                {
                    sb.Replace(i_str, string.Empty);
                }
                else
                {
                    int idxOfWord = sb.ToString().IndexOf(i_str);
                    sb.Remove(idxOfWord, cnt);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all instances of any number of characters from a specified string while ignoring case.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAllIgnoreCase(this string str, params IEnumerable<char>[] args)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            if (args == null) throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            str = str.ToLower();
            var sb = new StringBuilder(str);
            for (int i = 0; i < args.Length; i++)
            {
                sb.Replace(args[i].ToString().ToLower(), string.Empty);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all instances of any number of strings from a specified string while ignoring case.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAllIgnoreCase(this string str, params IEnumerable<string>[] args)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            if (args == null) throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            str = str.ToLower();
            var sb = new StringBuilder(str);
            for (int i = 0; i < args.Length; i++)
            {
                var cnt = args[i].Count();
                var i_str = args[i].ToString().ToLower();
                if (cnt == 1)
                {
                    sb.Replace(i_str, string.Empty);
                }
                else
                {
                    int idxOfWord = sb.ToString().IndexOf(i_str);
                    sb.Remove(idxOfWord, cnt);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Checks if a given string contains any digits.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string contains any digits, false otherwise</returns>
        public static bool ContainsDigits(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return false;
            }
            return str.Any(char.IsDigit);
        }

        /// <summary>
        /// Checks if a given string is a valid date used by System.DateTime
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="format">The date format regex</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string is a valid date recognized by System.DateTime</returns>
        public static bool IsSystemDateTime(this string date, string formattingRegex)
        {
            if (date == null) throw new ArgumentNullException(nameof(date));
            if (formattingRegex == null) throw new ArgumentNullException(nameof(formattingRegex));
            Contract.EndContractBlock();
            if (date.Length == 0 || formattingRegex.Length == 0)
            {
                return false;
            }
            return DateTime.TryParseExact(date, formattingRegex, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically greater than the previous character.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyIncreasing(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] > str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically greater than the previous character
        /// while ignoring case.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyIncreasingIgnoreCase(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] > str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically smaller than the previous character.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyDecreasing(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] < str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically smaller than the previous character
        /// while ignoring case.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyDecreasingIgnoreCase(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] > str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a given string is a palindrome.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string is a palindrome</returns>
        public static bool IsPalindrome(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return true;
            }

            //All palindromes that exist are less than Int32.MaxValue
            for (var advancing = 0; advancing < str.Length; advancing++)
            {
                int retrograding = str.Length - 1 - advancing;
                if (str[advancing] != str[retrograding])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if a given string is a palindrome while ignoring casing.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>True if the string is a palindrome</returns>
        public static bool IsPalindromeIgnoreCase(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return true;
            }

            //All palindromes that exist are less than Int32.MaxValue
            str = str.ToLower();
            for (var advancing = 0; advancing < str.Length; advancing++)
            {
                int retrograding = str.Length - 1 - advancing;
                if (str[advancing] != str[retrograding])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if a string is well formed. A string is well formed if
        /// for every alphabet-recognized character, there is an approriate
        /// closing character. For every inner string, with the exception
        /// of characters not defined in the alphabet, in between an opening
        /// and closing character, if that string were to be split in half,
        /// each half would be a mirror image of each other. A well formed 
        /// string consists of the default alphabet constists of the following 
        /// characters: '(',')','{','}','[',']','<','>'.
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>whether the string is well formed</returns>
        /// <example>The following demonstrates how to use the <see cref="IsWellFormed(string)"/> method.</example>
        /// <code>
        ///
        /// using static Utilities.StringUtils;
        /// 
        /// class TestClass
        /// {
        ///     static void Main(string[] args)
        ///     {
        ///         Console.WriteLine("<<()>>{}{}".IsWellFormed()); //prints true
        ///         Console.WriteLine("{([)]}".IsWellFormed()) //prints false
        ///     }
        /// }
        /// </code>
        public static bool IsWellFormed(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            return ZeroOrOne(str) ? false : IsWellFormed(str, WellFormedUtility.DefaultAlphabet);
        }

        /// <summary>
        /// Checks if a string is well formed. A string is well formed if
        /// for every alphabet-recognized character, there is an approriate
        /// closing character. For every inner string, with the exception
        /// of characters not defined in the alphabet, in between an opening
        /// and closing character, if that string were to be split in half,
        /// each half would be a mirror image of each other. A well formed 
        /// string consists of the user specified Dictionary of key-value
        /// pairs, where the key is the opening character and the value
        /// is the closing character.
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <param name="alphabet">the dictionary of key value pairs - where the key
        /// represents the opening character and the value represents the closing character</param>
        /// <returns>whether the string is well formed</returns>
        /// <example>The following demonstrates how to use the 
        /// <see cref="IsWellFormed(string, Dictionary{char, char})"/> method.</example>
        /// <code>
        ///
        /// using static Utilities.StringUtils;
        /// using System.Collections.Generic;
        ///
        /// class TestClass
        /// {
        ///     static void Main(string[] args)
        ///     {
        ///         var dict = new Dictionary<char, char>();
        ///         {
        ///             {'/','\'},
        ///             {'(',')'}
        ///         };
        ///         Console.WriteLine("(/Manu\)".IsWellFormed(dict)); //prints true
        ///     }
        /// }
        /// </code>
        public static bool IsWellFormed(this string str, Dictionary<char, char> alphabet)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            if (alphabet == null)
            {
                return IsWellFormed(str);
            }

            return new WellFormedUtility(alphabet).Run(str);
        }

        /// <summary>
        /// A utility class that contains functions to determine
        /// whether a string is a well formed string.
        /// </summary>
        [Author("Manu Puduvalli")]
        private class WellFormedUtility
        {
            /// <summary>
            /// The default alphabet
            /// </summary>
            public static Dictionary<char, char> DefaultAlphabet { get; }
                = new Dictionary<char, char>(4)
            {
                { '(', ')' },
                { '{', '}' },
                { '[', ']' },
                { '<', '>' },
            };

            /// <summary>
            /// An instance of the Dictionary containing this alphabet.
            /// </summary>
            private readonly Dictionary<char, char> Alphabet;

            /// <summary>
            /// Contructor that sets up the alphabet
            /// </summary>
            /// <param name="dct"></param>
            public WellFormedUtility(Dictionary<char,char> dct)
            {
                Alphabet = dct ?? throw new ArgumentNullException(nameof(dct));
            }

            /// <summary>
            /// Verifies the "well formedness" of the string by using
            /// a stack data structure to measure the balance of the string.
            /// </summary>
            /// <param name="inp">The input string</param>
            /// <returns></returns>
            public bool Run(string inp)
            {
                var stk = new Stack<char>(10);
                try
                {
                    foreach (var c in inp)
                    {
                        if (!Alphabet.ContainsKey(c) && !Alphabet.ContainsValue(c)) continue;
                        if (Alphabet.ContainsKey(c)) stk.Push(c);
                        else
                            if (Alphabet[stk.Pop()] != c)
                                return false;
                    }
                }
                catch (Exception ex) when (ex is InvalidOperationException ||
                                           ex is NullReferenceException)
                {
                    return false;
                }
                return true;
            }
        } //WellFormedUtilities

        /// <summary>
        /// Checks whether a string contains duplicate characters.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <returns>True if their are duplicate characters. False, otherwise</returns>
        public static bool ContainsDuplicateChars(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            var set = new HashSet<char>();
            for (int i = 0; i < str.Length; i++)
                //User overloaded operator for Add
                if (!set.Add(str[i]))
                    return false;
            return true;
        }

        /// <summary>
        /// Checks whether a string contains duplicate inner strings.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="arg">The inner string to search for duplicates</param>
        /// <returns>True if their are duplicate inner strings. False, otherwise</returns>
        public static bool ContainsDuplicateStrings(this string str, string arg)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return false;
            }

            if (arg == null)
            {
                return false;
            }

            var regex = new System.Text.RegularExpressions.Regex(
                pattern: System.Text.RegularExpressions.Regex.Escape(arg));
            var rem = regex.Replace(str, string.Empty, 1);
            var secondRem = rem.Replace(arg, string.Empty);
            return !(rem == secondRem);
        }

        /// <summary>
        /// Shuffle's characters in a string. The methodology used to generate random 
        /// indeces used for shuffling is cryptographically strong. Due to this nature,
        /// there is no guarantee that the return string will be entirely different 
        /// than the original.
        /// </summary>
        /// <param name="str">The string to be shuffled</param>
        /// <param name="preserveSpaces">Determines whether to shuffle spaces or not</param>
        /// <returns>The shuffled string</returns>
        public static string Shuffle(this string str, bool preserveSpaces = false)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (ZeroOrOne(str))
            {
                return str;
            }

            if (preserveSpaces)
            {
                if (str.Contains(SPACE))
                {
                    string[] spaceSplit = str.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                    for(int i = 0; i < spaceSplit.Length; i++)
                    {
                        if (!ZeroOrOne(spaceSplit[i]))
                        {
                            spaceSplit[i] = new ShuffleUtility(spaceSplit[i]).ShuffleThis();
                        }
                    }
                    return string.Join(" ", spaceSplit);
                }
            }
            return new ShuffleUtility(str).ShuffleThis();
        }

        /// <summary>
        /// A utility class to shuffle a string.
        /// </summary>
        [Author("Manu Puduvalli")]
        private protected class ShuffleUtility
        {
            private readonly string str;
            private readonly StringBuilder sb;

            /// <summary>
            /// Creates an instance of the ShuffleUtility and instantiates 
            /// any fields.
            /// </summary>
            /// <param name="str">The string to be shuffled</param>
            public ShuffleUtility(string str)
            {
                //Null Check because constructor is public and class is private protected 
                this.str = str ?? throw new ArgumentNullException(nameof(str));
                sb = new StringBuilder(str);
            }

            /// <summary>
            /// Conducts the shuffle. The methodology of the shuffle is simple while
            /// being less biased than System.Random. For each character that exists
            /// in the string, a swap occurs two times. Four characters are randomly
            /// chosen from the string and their indeces are stored. The index values
            /// occur from the range of [0, length). In addition, two randomly chosen 
            /// numbers are created in order to decide which two indeces are swapped 
            /// first. 
            /// </summary>
            /// <returns>The shuffled string</returns>
            /// <example>
            /// Here is a visual example of the shuffling algorithm described in the 
            /// summary:
            /// -- Note: In the loop below, the values in this example are chosen 
            ///    arbitrarily and will not necessarily be the same values on each 
            ///    iteration.
            ///    
            /// -- Assume the string -> "Thiscanbeareallylongstring"
            /// 
            /// -- loop begins: 
            /// 
            /// -- Four indeces (w, x, y, z) are chosen at random -> 3, 10, 7, 20
            /// 
            /// -- Two more random values (a, b) are chosen in the range of [1,4]
            ///    where each value represents one of the four index values.
            ///    
            /// -- Given every possible combination of [1,4] with combination size 's' 
            ///    where 's' equals 2, the random values 'a' and 'b' match a possible 
            ///    combination and perform two swaps.
            ///    
            ///         -- If value 'a' is 2 and value 'b' is 4 (or vice versa), then 
            ///            indeces 'x' and 'z' are swapped first, subsequently followed 
            ///            by the swap of indeces 'w' and 'y'.
            ///            
            ///         -- In the event that value 'a' and 'b' are the same value, 
            ///            the same random values used for generating 'a' and b'
            ///            are reused in order to generate 2 values (d, e) in the 
            ///            range, [1, 2]. The value 'd' decides whether 'a' or 'b'
            ///            will be changed value. For example, if 'd' evaluates
            ///            to 1, then value 'a' is guaranteed to change. If 'd' 
            ///            evaluates to 2, however, then value 'b' is guaranteed change.
            ///            Value 'e' decides whether to increment or decrement the number
            ///            in order to break the the tie between values 'a' and 'b'. 
            ///            If 'e' is 1, either 'a' or 'b' is decremented. If 'e' is 2,
            ///            either 'a' or 'b is incremented. Special consideration is taken
            ///            for the edge cases (if 'a' or 'b' holds the value 1 or 4).
            ///            
            /// -- The current state of the string is -> "Thibcanseaseallylongrtring
            /// 
            /// -- Loop 'n' times where 'n' is the size of the string
            /// 
            /// -- For each letter in the string, a maximum of four character swaps may occur. 
            ///    If swapping values are the same, then a swap does not occur.
            /// </example>
            public string ShuffleThis() {

                using (var rngcsp = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    var len = str.Length;

                    for (var i = 0; i < len; i++)
                    {
                        var _1 = new byte[8];
                        var _2 = new byte[8];
                        var _3 = new byte[8];
                        var _4 = new byte[8];

                        rngcsp.GetBytes(_1);
                        rngcsp.GetBytes(_2);
                        rngcsp.GetBytes(_3);
                        rngcsp.GetBytes(_4);
                        
                        var one     = (int)(Math.Abs(BitConverter.ToInt64(_1, 0)) % (len - 1) + 1);
                        var two     = (int)(Math.Abs(BitConverter.ToInt64(_2, 0)) % (len - 1) + 1);
                        var three   = (int)(Math.Abs(BitConverter.ToInt64(_3, 0)) % (len - 1) + 1);
                        var four    = (int)(Math.Abs(BitConverter.ToInt64(_4, 0)) % (len - 1) + 1);

                        var indexOne = new byte[8];
                        rngcsp.GetBytes(data: indexOne);
                        long longIndexOne = Math.Abs(BitConverter.ToInt64(indexOne, startIndex: 0));

                        var indexTwo = new byte[8];
                        rngcsp.GetBytes(data: indexTwo);
                        long longIndexTwo = Math.Abs(BitConverter.ToInt64(indexTwo, startIndex: 0));

                        var randOne = (int)(longIndexOne % 4 + 1);
                        var randTwo = (int)(longIndexTwo % 4 + 1);

                        if(randOne == randTwo)
                        {
                            var chooser = (int)(longIndexOne % 2 + 1);
                            var changer = (int)(longIndexTwo % 2 + 1);

                            if(chooser == 1)
                            {
                                if (randOne == 1) randOne++;
                                else if (randOne == 4) randOne--;
                                else randOne = changer == 1 ? randOne - 1 : randOne + 1;
                            }
                            else // 2
                            {
                                if (randTwo == 1) randTwo++;
                                else if (randTwo == 4) randTwo--;
                                else randTwo = changer == 1 ? randTwo - 1 : randTwo + 1;
                            }
                        }

                        if ((randOne == 1 && randTwo == 2) || (randOne == 2 && randTwo == 1))
                        {
                            ShuffleSwapper(sb, one, two, three, four);
                        }
                        else if ((randOne == 1 && randTwo == 3) || (randOne == 3 && randTwo == 1))
                        {
                            ShuffleSwapper(sb, one, three, three, four);
                        }
                        else if ((randOne == 1 && randTwo == 4) || (randOne == 4 && randTwo == 1))
                        {
                            ShuffleSwapper(sb, one, four, two, three);
                        }
                        else if ((randOne == 2 && randTwo == 3) || (randOne == 3 && randTwo == 2))
                        {
                            ShuffleSwapper(sb, two, three, one, four);
                        }
                        else if ((randOne == 2 && randTwo == 4) || (randOne == 4 && randTwo == 2))
                        {
                            ShuffleSwapper(sb, two, four, one, three);
                        }
                        else
                        {
                            ShuffleSwapper(sb, three, four, one, two);
                        }
                    }
                }
                return sb.ToString();
            }
            private void ShuffleSwapper(StringBuilder sb, int indexOne, int indexTwo, int indexThree, int indexFour)
            {
                //Contract.Requires<ArgumentNullException>(sb != null);
                //Contract.Requires((indexOne >= 0) && (indexTwo >= 0) && (indexThree >= 0) && (indexFour >= 0));
                Contract.EndContractBlock();

                if (indexOne != indexTwo)
                {
                    char tmp = sb[indexOne];
                    sb[indexOne] = sb[indexTwo];
                    sb[indexTwo] = tmp;
                }
                if (indexThree != indexFour)
                {
                    char tmp2 = sb[indexThree];
                    sb[indexThree] = sb[indexFour];
                    sb[indexFour] = tmp2;
                }
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static bool ZeroOrOne(string str) => str.Length == 0 || str.Length == 1;
    } //StringUtils 
} //Note
