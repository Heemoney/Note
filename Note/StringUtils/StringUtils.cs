﻿using Note.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Note.Strings
{
    [Author("Manu Puduvalli")]
    public static class StringUtils
    {
        private const char C_SP = (char)32;

        /// <summary>
        /// Converts a <see cref="IEnumerable{char}"/> to a string
        /// </summary>
        /// <param name="charEnumerable">The <see cref="IEnumerable{char}"/></param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="charEnumerable"/> is null</exception>
        /// <returns>The converted string</returns>
        public static string CharToString(IEnumerable<char> charEnumerable)
        {
            if(charEnumerable is null) throw new ArgumentNullException(nameof(charEnumerable));
            return new string(charEnumerable as char[] ?? charEnumerable.ToArray());
        }

        /// <summary>
        /// Creates a string from the first character of the string to the first whitespace.
        /// </summary>
        /// <param name="str">The string to be chomped</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>The string retaining the first word</returns>
        public static string Chomp(this string str)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (str.Length == 0)
            {
                return str;
            }

            int idx = str.IndexOf(string.Empty, StringComparison.InvariantCulture);
            if (idx >= 0)
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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>The string retaining the chomped word</returns>
        public static string ChompAfter(this string str, int spaces)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return str;
            }
            if (spaces != 0)
            {
                var matching_num_spaces = 0;
                for (var i = 0; i < str.Length; i++)
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
        /// Checks if a given string contains any digits.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>True if the string contains any digits, false otherwise</returns>
        public static bool ContainsDigits(this string str)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return false;
            }
            return str.Any(char.IsDigit);
        }

        /// <summary>
        /// Checks whether a string contains duplicate characters.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>True if their are duplicate characters. False, otherwise</returns>
        public static bool ContainsDuplicateChars(this string str)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return false;
            }

            var set = new HashSet<char>();
            for (var i = 0; i < str.Length; i++)
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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="arg"/> is null</exception>
        /// <returns>True if their are duplicate inner strings. False, otherwise</returns>
        public static bool ContainsDuplicateStrings(this string str, string arg)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return false;
            }

            if (arg is null)
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
        /// Counts how many times a given letter appears in a string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="letter">The specfic letter to search</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns></returns>
        public static int CountLetters(this string str, char letter)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return 0;
            }
            return (from chars in str where chars == letter select chars).Count();
        }

        /// <summary>
        /// Counts the number of words in a string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>The number of words in the string</returns>
        public static int CountWords(this string str)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return 0;
            }
            if (str.Length == 1)
            {
                return 1;
            }
            return str.Split(new char[] { C_SP, '\r', '\n' },
                options: StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Checks if a given string is a palindrome.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="ignoreCase">Whether case should be ignored</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>True if the string is a palindrome</returns>
        public static bool IsPalindrome(this string str, bool ignoreCase = false)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return true;
            }

            if (ignoreCase)
            {
                str = str.ToUpperInvariant();
            }
            int j;
            for (var i = 0; i < str.Length; i++)
            {
                j = str.Length - 1 - i;
                if (str[i] != str[j])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically smaller than the previous character.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="ignoreCase">Whether case should be ignored</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyDecreasing(this string str, bool ignoreCase = false)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return false;
            }

            if (ignoreCase)
            {
                str = str.ToUpperInvariant();
            }

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] < str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if each character in a string is lexicographically greater than the previous character.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="ignoreCase">Whether case should be ignored</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>True if the string strictly increases</returns>
        public static bool IsStrictlyIncreasing(this string str, bool ignoreCase = false)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return false;
            }

            if (ignoreCase)
            {
                str = str.ToUpperInvariant();
            }

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] > str[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a given string is a valid date used by System.DateTime
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="formattingRegex">The date format regex</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="date"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="formattingRegex"/> is null</exception>
        /// <returns>True if the string is a valid date recognized by System.DateTime</returns>
        public static bool IsSystemDateTime(this string date, string formattingRegex)
        {
            date = date ?? throw new ArgumentNullException(nameof(date));
            if (formattingRegex is null) throw new ArgumentNullException(nameof(formattingRegex));
            Contract.EndContractBlock();
            if (date.Length == 0 || formattingRegex.Length == 0)
            {
                return false;
            }

            return DateTime.TryParseExact(date, formattingRegex, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// Checks if a given string is a valid URI. This checks both HTTP and HTTPS URLs.
        /// </summary>
        /// <param name="uri">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="uri"/> is null</exception>
        /// <returns>True if the URI is valid</returns>
        public static bool IsValidURI(string uri)
        {
            uri = uri ?? throw new ArgumentNullException(nameof(uri));
            Contract.EndContractBlock();
            return Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
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
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            return IsZeroOrOne(str) ? false : IsWellFormed(str, WellFormedUtility.DefaultAlphabet);
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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
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
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (IsZeroOrOne(str))
            {
                return false;
            }

            if (alphabet is null)
            {
                return IsWellFormed(str);
            }

            return new WellFormedUtility(alphabet).Run(str);
        }

        /// <summary>
        /// Finds the longest common prefix of a group of strings of type 
        /// <see cref="IEnumerable{string}"/>
        /// </summary>
        /// <param name="strs">A group of strings to find the common prefix</param>
        /// <returns>The longest common prefix</returns>
        public static string LongestCommonPrefix(this IEnumerable<string> strs, bool ignoreCase = false)
        {
            int FindMin()
            {
                var min = strs.ElementAt(0).Length;
                foreach (string s in strs)
                {
                    if (s.Length < min)
                        min = s.Length;
                }
                return min;
            }

            bool CheckContain(string str, int st, int end)
            {
                for (var i = 0; i < strs.Count(); i++)
                {
                    string word = strs.ElementAt(i);
                    if (ignoreCase)
                    {
                        word = word.ToUpperInvariant();
                        str = str.ToUpperInvariant();
                    }
                    for (int j = st; j <= end; j++)
                    {
                        if (word[j] != str[j])
                            return false;
                    }
                }
                return true;
            }

            if (strs is null) throw new ArgumentNullException(nameof(strs));
            Contract.EndContractBlock();
            if (strs.Count() == 1) return strs.ElementAt(0);

            int minLength = FindMin();
            var sb = new StringBuilder();
            string mainWord = strs.ElementAt(0);

            int low = 0, high = minLength - 1;
            while (low <= high)
            {
                var mid = low + (high - low) / 2;
                if (CheckContain(mainWord, low, mid))
                {
                    sb.Append(mainWord.Substring(low, (mid + 1) - low));
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Orders an enumerable by its length in ascending order (natural order).
        /// </summary>
        /// <param name="si">the <see cref="IEnumerable{string}"/> si</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>An ordered enumerable</returns>
        public static IEnumerable<string> OrderByLength(this IEnumerable<string> si)
        {
            si = si ?? throw new ArgumentNullException(nameof(si), Properties.Resources.EX_PARAM_NULL);
            return from strs in si orderby strs.Length ascending select strs;
        }

        /// <summary>
        /// Removes all instances of any number of characters from a specified string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="ignoreCase">Whether case should be ignored</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="args"/> is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAll(this string str, bool ignoreCase = false, params IEnumerable<char>[] args)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            args = args ?? throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            if (ignoreCase)
            {
                str = str.ToUpperInvariant();
            }
            var sb = new StringBuilder(str);
            for (var i = 0; i < args.Length; i++)
            {
                sb.Replace(args[i].ToString(), string.Empty);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all instances of any number of strings from a specified string.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="ignoreCase">Whether case should be ignored</param>
        /// <param name="args">The characters which will be removed</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="args"/> is null</exception>
        /// <returns>The string with all characters in args removed</returns>
        public static string RemoveAll(this string str, bool ignoreCase = false, params IEnumerable<string>[] args)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            args = args ?? throw new ArgumentNullException(nameof(args));
            Contract.EndContractBlock();
            if (args.Length == 0 || str.Length == 0)
            {
                return str;
            }
            if (ignoreCase)
            {
                str = str.ToUpperInvariant();
            }
            var sb = new StringBuilder(str);

            for (var i = 0; i < str.Length; i++)
            {
                int cnt = args[i].Count();
                var i_str = args[i].ToString();
                if (cnt == 1)
                {
                    sb.Replace(i_str, string.Empty);
                }
                else
                {
                    int idxOfWord = sb.ToString().IndexOf(i_str, StringComparison.InvariantCulture);
                    sb.Remove(idxOfWord, cnt);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Replaces a character at a specific index in a string, only once.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <param name="c">The character to replace</param>
        /// <param name="index">The index to replace <paramref name="c"/></param>
        public static string ReplaceAt(this string str, int index, char c)
        {
            str = str ?? throw new ArgumentNullException(nameof(str), Properties.Resources.EX_PARAM_NULL);
            Contract.EndContractBlock();

            var sb = new StringBuilder(str);
            sb[index] = c;
            return sb.ToString();
        }

        /// <summary>
        /// Reverses a string from left to right order while maintaining case sensitivity.
        /// </summary>
        /// <param name="str">The string to be reversed</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="str"/> is null</exception>
        /// <returns>The reversed string</returns>
        public static string Reverse(this string str)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

            if (str.IsZeroOrOne())
            {
                return str;
            }

            var c = str.ToCharArray();
            Array.Reverse(c);
            return new string(value: c);
        }

        /// <summary>
        /// Shuffle's characters in a string. The methodology used to generate random
        /// indeces used for shuffling is cryptographically strong. Due to this nature,
        /// there is no guarantee that the return string will be entirely different
        /// than the original.
        /// </summary>
        /// <param name="str">The string to be shuffled</param>
        /// <param name="preserveSpaces">Determines whether to shuffle spaces or not</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="str"/> is null</exception>
        /// <returns>The shuffled string</returns>
        public static string Shuffle(this string str, bool preserveSpaces = false)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            if (str.IsZeroOrOne())
            {
                return str;
            }

            if (preserveSpaces)
            {
                if (str.Contains(C_SP))
                {
                    var spaceSplit = str.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < spaceSplit.Length; i++)
                    {
                        if (!spaceSplit[i].IsZeroOrOne())
                        {
                            var shuffleUtil = new Common.ShuffleUtil<char>(spaceSplit[i].ToCharArray());
                            spaceSplit[i] = new string(shuffleUtil.ShuffleThis());
                        }
                    }
                    return string.Join(" ", spaceSplit);
                }
            }
            return new string(new Common.ShuffleUtil<char>(str.ToCharArray()).ShuffleThis());
        }

        /// <summary>
        /// Performs a Substring given a starting and ending index, similar to Java.
        /// The operation is performed mathematically as [startIndex, endIndex).
        /// </summary>
        /// <param name="str">The given string</param>
        /// <param name="startIndex">The inclusive starting index of <paramref name="str"/></param>
        /// <param name="endIndex">The exclusive ending index of <paramref name="str"/></param>
        /// <returns>A string that is equivalent to the substring that begins at startIndex in this 
        /// instance, or Empty if startIndex is equal to the length of this instance.</returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string Substring(this string str, int startIndex, int endIndex)
        {
            str = str ?? throw new ArgumentNullException(nameof(str));
            return str.Substring(startIndex, (endIndex - startIndex) + 1);
        }

        /// <summary>
        /// Returns true if the length of the string is zero or one.
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <returns>True if the length of the string is zero or one</returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static bool IsZeroOrOne(this string str) => str.Length == 0 || str.Length == 1;

        
        /// <summary>
        /// A utility class that contains functions to determine
        /// whether a string is a well formed string.
        /// </summary>
        [Author("Manu Puduvalli")]
        private class WellFormedUtility
        {
            /// <summary>
            /// An instance of the Dictionary containing this alphabet.
            /// </summary>
            private readonly Dictionary<char, char> Alphabet;

            /// <summary>
            /// Contructor that sets up the alphabet
            /// </summary>
            /// <param name="dct">A dictionary representing an alphabet</param>
            public WellFormedUtility(Dictionary<char, char> dct)
            {
                Alphabet = dct ?? throw new ArgumentNullException(nameof(dct));
            }

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
                        else if (Alphabet[stk.Pop()] != c)
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
    } //StringUtils
} //Note
