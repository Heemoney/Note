using System;
using System.Linq;
using System.Text;
using Note.Attributes;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Threading;

namespace Note
{
    [Author("Manu Puduvalli")]
    [Author("Sam Yuen")]
    public static class EnumerableUtils
    {
        /// <summary>
        /// Concatenates all enumerables which are specified in in the parameter. The
        /// concatenation occurs in the order specified in the parameter.
        /// </summary>
        /// <typeparam name="T">The enumerable type to be used</typeparam>
        /// <param name="ie">An enumerable of all one dimensional arrays to be concatenated</param>
        /// <exception cref="ArgumentNullException"> Is thrown if any enumerable, which is a candidate to be concatenated, is null</exception>
        /// <returns>A single enumerable with all of the concatenated elements</returns>
        /// <example>This simple example shows how to call the <see cref="ConcatAny{T}(IEnumerable{T}[])"/> method.</example>
        /// <code>
        ///
        /// using static Utilities.EnumerableUtils;
        ///
        /// class TestClass
        /// {
        ///    static void Main(string[] args)
        ///    {
        ///        int[] x = { 1, 2, 3, 4 };
        ///        int[] y = { 1, 2, 3, 4, 5, 6 };
        ///        int[] z = { 1, 2, 3 };
        ///        int[] comb = ConcatAny(x, y, z).toArray();
        ///        //Printing out 'comb' results in 1, 2, 3, 4, 1, 2, 3, 4, 5, 6, 1, 2, 3
        ///    }
        /// }
        /// </code>
        public static IEnumerable<T> ConcatAny<T>(params IEnumerable<T>[] ie) //Passing a variable number of IEnumerable's as params
        {
            if (ie.Any(x => x == null)) throw new ArgumentNullException("One of the params array's were null");
            Contract.EndContractBlock();

            var arrTotal = 0;
            foreach (IEnumerable<T> t in ie)
            {
                arrTotal += t.Count();
            }
            var z = new T[arrTotal];

            var dest = 0;
            foreach (IEnumerable<T> t in ie)
            {
                Array.ConstrainedCopy(t.ToArray(), 0, z, dest, t.Count());
                dest += t.Count();
            }
            return z;
        }

        /// <summary>
        /// Adds all the values in an IEnumerable.
        /// </summary>
        /// <param name="src">the IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <returns>The sum of all elements in the source</returns>
        [Beta]
        public static int AddAll(this IEnumerable<int> src)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            Contract.EndContractBlock();

            if (src.Count() == 1)
            {
                return src.ElementAt(0);
            }
            var arr_mult_tot = src.Aggregate(1, (idx1, idx2) => idx1 + idx2);
            return arr_mult_tot;
        }

        /// <summary>
        /// A generic overload of the <see cref="AddAll(IEnumerable{int})"/> method. This method will call
        /// the <see cref="AddAll(IEnumerable{int})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The sum of all elements in the source</returns>
        public static int AddAll<T>(this IEnumerable<T> numbers, Func<T, int> selector)
        {
            return (from num in numbers select selector(num)).AddAll();
        }

        /// <summary>
        /// Subtracts all the values in an IEnumerable.
        /// </summary>
        /// <param name="src">the IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <returns>The difference of all elements in the source</returns>
        [Beta]
        public static int SubtractAll(this IEnumerable<int> src)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            Contract.EndContractBlock();

            if (src.Count() == 1)
            {
                return src.ElementAt(0);
            }
            var arr_mult_tot = src.Aggregate(0, (idx1, idx2) => idx1 - idx2);
            return arr_mult_tot;
        }

        /// <summary>
        /// A generic overload of the <see cref="SubtractAll(IEnumerable{int})"/> method. This method will call
        /// the <see cref="SubtractAll(IEnumerable{int})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The difference of all elements in the source</returns>
        public static int SubtractAll<T>(this IEnumerable<T> numbers, Func<T, int> selector)
        {
            return (from num in numbers select selector(num)).SubtractAll();
        }

        /// <summary>
        /// Returns whether an IEnumerable is null or empty
        /// </summary>
        /// <typeparam name="T">The type of the IEnumerable</typeparam>
        /// <param name="ie">The IEnumerable to be used</param>
        /// <returns>The truth</returns>
        public static bool IsNullOrEmpty<T>(IEnumerable<T> ie) => ie == null || ie.Count() == 0;

        /// <summary>
        /// Inserts the specified element at the specified index in the enumerable (modifying the original enumerable).
        /// If element at that position exits, If shifts that element and any subsequent elements to the right,
        /// adding one to their indices. The method also allows for inserting more than one element into
        /// the enumerable at one time given that they are specified. This Insert method is functionally similar
        /// to the Insert method of the List class. <see cref="System.Collections.IList.Insert(int, object)"/>
        /// for information about the add method of the List class.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="src">The IEnumerable to be used</param>
        /// <param name="startIdx">The index to start insertion</param>
        /// <param name="amtToIns">The amount of elements to insert into the enumerable</param>
        /// <param name="valuesToIns">Optionally, the values to insert into the empty indices of the new enumerable</param>
        /// <returns>An enumerable of the elements inserted into the enumerable, if any</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the valuesToIns enumerable does not match the amount to insert (if it is greater than 0)</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when the amtToIns or the startIdx is less than 0</exception>
        /// <example>This sample shows how to call the <see cref="EnumerableUtils.Insert{T}(T[], int, int, T[])"/> method.</example>
        /// <seealso cref="System.Collections.IList.Insert(int, object)"/>
        /// <code>
        ///
        /// using static Utilities.EnumerableUtils;
        ///
        /// class TestClass
        /// {
        ///     static void Main(string[] args)
        ///     {
        ///         var w = new int[9] {2, 3, 4, 5, 6, 7, 8, 9, 10}.AsEnumerable();
        ///         InsertInto(ref w, 1, 3);
        ///         //Printing out 'w' results in: 2, 0, 0, 0, 3, 4, 5, 6, 7, 8, 9, 10
        ///
        ///         var y = new int[9] {2, 3, 4, 5, 6, 7, 8, 9, 10}.AsEnumerable();
        ///         InsertInto(ref y, 1, 3, 250, 350, 450);
        ///         //Printing out 'y' results in: 2, 250, 350, 450, 3, 4, 5, 6, 7, 8, 9, 10
        ///     }
        /// }
        /// </code>
        public static IEnumerable<T> InsertInto<T>(ref IEnumerable<T> src, int startIdx, int amtToIns, params T[] valuesToIns)
        {
            int len = src.Count();

            if (src == null || valuesToIns == null)
            {
                throw new ArgumentNullException();
            }
            if (startIdx < 0 || startIdx >= len || amtToIns < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (amtToIns != valuesToIns.Length && valuesToIns.Length != 0)
            {
                throw new IndexOutOfRangeException("offset amount should equal the number of values to be filled");
            }
            var arr_managed = new T[len + amtToIns];
            Contract.Ensures(Contract.Result<T[]>() != null);
            Contract.EndContractBlock();

            if (amtToIns != 0)
            {
                if (startIdx == len - 1)
                {
                    Array.ConstrainedCopy(src.ToArray(), 0, arr_managed, 0, len);
                    Array.ConstrainedCopy(valuesToIns, 0, arr_managed, startIdx + 1, valuesToIns.Length);
                }
                else
                {
                    Array.ConstrainedCopy(src.ToArray(), 0, arr_managed, 0, startIdx);
                    Array.ConstrainedCopy(valuesToIns, 0, arr_managed, startIdx, valuesToIns.Length);
                    Array.ConstrainedCopy(src.ToArray(), startIdx, arr_managed, startIdx + amtToIns, len - startIdx);
                }
            }
            src = arr_managed;
            return valuesToIns;
        }

        /// <summary>
        /// Prints a string representation of an enumerable. There are 4 supported lengths for the formattingRegex. The
        /// default length is 0 and the default behavior depends on the type of the enumerable. If the type is primitive
        /// (based on the System.IsPrimitive property) including decimal and string, then it prints the enumerable with a space
        /// as a separator between each element. If the enumerable is not primitive, it prints the enumerable with no separator.
        /// A formattingRegex of length 1 specifies a character to separate each element. The enumerable is printed out, following
        /// a default behavior, execpt with the specified separator rather than the default separator. A formattingRegex
        /// of length 2 specifies a two characters to mark the left and right outer bounds of the enumerable, A formattingRegex
        /// of length 3 specifies a character for the left outer bound of the enumerable, followed by a separator character,
        /// followed by a character for the right outer bound of the enumerable. If no separator is desired, the "/0+" regex
        /// can be specified.The evenlySpacedSeparator parameter specifies whether an even number of spaces should be on
        /// both sides of the separator. This parameter ignores Object type enumerable's excluding decimal and string.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="src">The IEnumerable to be used</param>
        /// <param name="formattingRegex">The guidelined regex to be optionally used</param>
        /// <param name="evenlySpacedSeparator">Determines whether the spacing between each element should be the same</param>
        /// <returns>The string representation of the enumerable</returns>
        /// <exception cref="ArgumentException">If arr is null</exception>
        /// <exception cref="FormatException">If the formatting regex length is neither 0 or 3</exception>
        /// <example>This sample shows how to call the <see cref="ToStringX{T}(T[], string, bool)"/> method.</example>
        /// <code>
        /// 
        /// using static Utilities.EnumerableUtils;
        /// 
        /// class TestClass
        /// {
        ///     static void Main(string[] args)
        ///     {
        ///         var w = new int[9] {2, 3, 4, 5, 6, 7, 8, 9, 10};
        ///         Console.WriteLine(w.ToStringX("[,]"));
        ///         //Printing out 'w' results in: [2, 3, 4, 5, 6, 7, 8, 9, 10]
        ///
        ///         var x = new int[9] {2, 3, 4, 5, 6, 7, 8, 9, 10};
        ///         Console.WriteLine(x.ToStringX("(|)", true));
        ///         //Printing out 'x' results in: (2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10)
        ///
        ///         var y = new string[4] {"Bill", "Bob", "Tom", "Joe"};
        ///         Console.WriteLine(y.ToStringX());
        ///         //Printing out 'y' results in: Bill Bob Tom Joe
        ///     }
        /// }
        /// </code>
        public static string ToStringX<T>(this IEnumerable<T> src, string formattingRegex = "", bool evenlySpacedSeparator = false)
        {
            int frl = formattingRegex.Length;

            if (src == null) throw new ArgumentNullException(nameof(src));

            if (frl < 0 || frl > 3)
            {
                throw new FormatException("Unsupported Regular Expression");
            }

            Contract.Ensures(Contract.Result<T[]>() != null);
            Contract.EndContractBlock();

            string outerLeft = string.Empty, separator = string.Empty, outerRight = string.Empty;
            bool hasNoSep = false;
            if (formattingRegex.Equals("/0+"))
            {
                hasNoSep = true;
                frl = 1;
            }

            switch (frl)
            {
                case 3:
                    outerLeft = formattingRegex[0].ToString();
                    separator = formattingRegex[1].ToString();
                    outerRight = formattingRegex[2].ToString();
                    break;

                case 2:
                    outerLeft = formattingRegex[0].ToString();
                    outerRight = formattingRegex[1].ToString();
                    break;

                case 1:
                    separator = formattingRegex[0].ToString();
                    break;
            }

            bool isLooselyPrimitive = false;
            var T_type = typeof(T);
            if (T_type.IsPrimitive || T_type == typeof(decimal) || T_type == typeof(string))
            {
                isLooselyPrimitive = true;
            }

            var sb = new StringBuilder();

            sb.Append(outerLeft);

            int len = src.Count();

            for (int i = 0; i < len; i++)
            {
                if (i == len - 1)
                {
                    sb.Append(src.ElementAt(i));
                }
                else switch (frl)
                    {
                        case 0:
                        case 2:
                        case 3:
                        defBehavior:
                            if (evenlySpacedSeparator && isLooselyPrimitive)
                            {
                                if (frl != 2)
                                {
                                    sb.Append(src.ElementAt(i) + " " + separator + " ");
                                }
                                else
                                {
                                    sb.Append(src.ElementAt(i) + separator + " ");
                                }
                            }
                            else if (isLooselyPrimitive)
                            {
                                sb.Append(src.ElementAt(i) + separator + " ");
                            }
                            else
                            {
                                sb.Append(src.ElementAt(i) + separator);
                            }
                            break;

                        case 1:
                            if (hasNoSep)
                                sb.Append(src.ElementAt(i));
                            else
                                goto defBehavior;
                            break;
                    }
            }
            sb.Append(outerRight);
            return sb.ToString();
        }

        [StrongBeta]
        public static void Copy<T>(IEnumerable<T> sourceEnumerable,
                                   int sourceIndex,
                                   IEnumerable<T> destEnumerable,
                                   int destIndex,
                                   int length) {
            HyperIterator.HyperIteratorUnm(sourceEnumerable, sourceIndex, destEnumerable, destIndex, length);
        }

        [StrongBeta]
        public static void Copy<T>(IEnumerable<T> sourceEnumerable, IEnumerable<T> destEnumerable, int length)
        {
            Copy(sourceEnumerable, 0, destEnumerable, 0, length);
        }

        [StrongBeta]
        public static void Copy<T>(IEnumerable<T> sourceEnumerable, IEnumerable<T> destEnumerable)
        {
            int len = destEnumerable.Count();
            Copy(sourceEnumerable, 0, destEnumerable, 0, len);
        }
        
        internal static bool LibCopy(int[] x, int[] y)
        {
            Array.Copy(x, y, x.Length);
            return true;
        }

        [StrongBeta]
        [System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.MayCorruptInstance, System.Runtime.ConstrainedExecution.Cer.MayFail)]
        internal static class HyperIterator
        {
            private const int THREAD_CAP = 50;
            private const int THREAD_MIN = 1;
            internal static void HyperIteratorUnm<T>(IEnumerable<T> src, int srcIdx, IEnumerable<T> dst, int destIdx, int len)
            {
                short numThreads = (short)(Math.Log(len) / 2);

                if (numThreads == THREAD_MIN - 1 || numThreads == THREAD_MIN || numThreads == THREAD_MIN + 1)
                {
                    Array.Copy((T[])src, srcIdx, (T[]) dst, destIdx, len);
                    return;
                }
                if (numThreads > THREAD_CAP)
                {
                    numThreads = THREAD_CAP;
                }

                double threadApproxPartition = (double)len / numThreads;
                int threadDefPartition = (int)Math.Floor(threadApproxPartition);
                bool obtusePartition = len % 2 == 0 && (len / threadDefPartition) % 2 != 0;

                Thread[] t_harness = new Thread[numThreads];
                int offset = 0;
                for (int i = 0; i < numThreads; i++)
                {
                    if (i == numThreads - 1 && obtusePartition)
                    {
                        threadDefPartition = len - offset;
                    }

                    T[] isolatedPartitionSrc = new T[threadDefPartition];
                    Array.Copy((T[])src, srcIdx + offset, isolatedPartitionSrc, 0, threadDefPartition);

                    int offCap = offset;
                    int tdpCap = threadDefPartition;
                    t_harness[i] = new Thread(() =>
                        Array.Copy(isolatedPartitionSrc, 0, (T[])dst, destIdx + offCap, tdpCap))
                    {
                        IsBackground = true
                    };
                    t_harness[i].Start();

                    offset += threadDefPartition;
                }

                for (int i = 0; i < numThreads; i++)
                {
                    t_harness[i].Join();
                }
            }
        }//HyperIterator
    }//EnumerableUtils
}//Namespace
