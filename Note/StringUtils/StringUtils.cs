﻿using System;
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
            if(str.Length == 0)
            {
                return string.Empty;
            }
            var c = str.ToCharArray();
            Array.Reverse(c);
            return new string(c);
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
                return string.Empty;
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
                return string.Empty;
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
            if (args.Length == 0)
            {
                return str;
            }
            if (str.Length == 0)
            {
                return string.Empty;
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
            if (args.Length == 0)
            {
                return str;
            }
            if (str.Length == 0)
            {
                return string.Empty;
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
            if (args.Length == 0)
            {
                return str;
            }
            if (str.Length == 0)
            {
                return string.Empty;
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
            if (args.Length == 0)
            {
                return str;
            }
            if (str.Length == 0)
            {
                return string.Empty;
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
        /// Roughly mimics C style strings in that each character of the specified string is stored in a List
        /// </summary>
        /// <param name="str">The string to be used</param>
        /// <exception cref="ArgumentNullException">Thrown when the string is null</exception>
        /// <returns>The string represented as a List</returns>
        public static List<char> ToList(this string str)
        {
            if(str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            if (str.Length == 0)
            {
                return new List<char>(0);
            }
            var strcpy_list = new List<char>(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                strcpy_list.Add(str[i]);
            }
            return strcpy_list;
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
            if (str.Length == 0)
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
            if (str.Length == 0)
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
            if (str.Length == 0)
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
            if (str.Length == 0)
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
            if (str.Length == 0)
            {
                return true; //empty string is a palindrome!
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
            if (str.Length == 0)
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
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>whether the string is well formed</returns>
        public static bool IsWellFormed(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();
            return IsWellFormed(str, WellFormedUtility.DefaultAlphabet);
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
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <param name="alphabet">the dictionary of key value pairs - where the key
        /// represents the opening character and the value represents the closing character</param>
        /// <returns>whether the string is well formed</returns>
        public static bool IsWellFormed(this string str, Dictionary<char, char> alphabet)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            Contract.EndContractBlock();

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
        protected internal class WellFormedUtility
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
                //Because the Utility is a protected internal class
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

            if (arg == null) return false;
            var regex = new System.Text.RegularExpressions.Regex(
                pattern: System.Text.RegularExpressions.Regex.Escape(arg));
            var rem = regex.Replace(str, string.Empty, 1);
            var secondRem = rem.Replace(arg, string.Empty);
            return !(rem == secondRem);
        }
    } //StringUtils 
} //Note
