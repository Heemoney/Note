using System;
using System.Collections.Generic;
using System.Linq;
using Note.UtilExceptions.EnumberableStatsExceptions;
using Note.Attributes;
using System.Diagnostics.Contracts;

namespace Note.Enumberables
{
    [Author("Sam Yuen")]
    [Author("Manu Puduvalli")]
    public static class EnumerableStats
    {
        /*
         * The tolerance level (upper and lower limits)
         */
        private const double TOLERANCE = 0.00000000000000000000;

        /// <summary>
        /// Finds the average of all the elements in the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>The average of all the elements in the source</returns>
        private static double Mean(this IEnumerable<double> src)
        {
            src = src ?? throw new ArgumentNullException(nameof(src));
            int len = src.Count();

            if (len == 0)
            {
                throw new InvalidOperationException(nameof(src), null);
            }

            Contract.EndContractBlock();

            if (src.Count() == 1)
            {
                return src.ElementAt(0);
            }
            return src.Mean();
        }

        /// <summary>
        /// A generic overload of the <see cref="Mean(IEnumerable{double})"/> method. This method will call
        /// the <see cref="Mean(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The average of all the elements in the source</returns>
        public static double Mean<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).Mean();
        }

        /// <summary>
        /// Finds the median of all the elements in the enumerable.
        /// </summary>
        /// <param name="a">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source if of length 0</exception>
        /// <returns>The median of the source</returns>
        private static double Median(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));

            Contract.EndContractBlock();
            if (len == 1)
            {
                return src.ElementAt(0);
            }

            var sorted = from num in src orderby num select num;
            var itemIndex = sorted.Count() / 2;

            if (sorted.Count() % 2 == 0)
                return (sorted.ElementAt(itemIndex) + sorted.ElementAt(itemIndex - 1)) / 2;
            else
                return sorted.ElementAt(itemIndex);
        }

        /// <summary>
        /// A generic overload of the <see cref="Median(IEnumerable{double})"/> method. This method will call
        /// the <see cref="Median(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The median of the source</returns>
        public static double Median<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).Median();
        }

        /// <summary>
        /// Finds the mode of all the elements in the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <exception cref="NoModeException">Thrown when no mode exists for the source</exception>
        /// <returns>The mode of the source</returns>
        public static double Mode(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));

            Contract.EndContractBlock();
            if (len == 1)
            {
                return src.ElementAt(0);
            }

            var sorted = from num in src orderby num select num;

            var hash = new Dictionary<double, int>();
            foreach (var t in sorted)
            {
                if (hash.ContainsKey(t)) hash[t] += 1;
                else hash.Add(t, 1);
            }

            var keysInDict = hash.Keys.ToArray();
            var numOfMode = hash.Values.ToArray();

            var max = numOfMode[0];
            var index = 0;
            var allEqual = 0;

            for (var i = 0; i < hash.Count; i++)
            {
                if (max < numOfMode[i])
                {
                    max = numOfMode[i];
                    index = i;
                }
                if (max == numOfMode[i]) allEqual++;
            }

            if (allEqual != numOfMode.Length)
                return keysInDict[index];
            throw new NoModeException("No Mode Present");
        }

        /// <summary>
        /// A generic overload of the <see cref="Mode(IEnumerable{double})"/> method. This method will call
        /// the <see cref="Mode(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The mode of the source</returns>
        public static double Mode<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).Mode();
        }

        /// <summary>
        /// Finds the sample variance of the enumerable.
        /// </summary>
        /// <param name="a">The IEnumberable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source if of length 0</exception>
        /// <returns>The sample variance of the source</returns>
        public static double SampleVariance(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            double mean = Mean(src);
            var sum = 0.0;
            for (var i = 0; i < len; i++)
            {
                sum += Math.Pow(src.ElementAt(i) - mean, 2);
            }
            sum /= (len - 1); //n-1 denoted as bessel's correction for sample standard dev

            return sum;
        }

        /// <summary>
        /// A generic overload of the <see cref="SampleVariance(IEnumerable{double})"/> method. This method will call
        /// the <see cref="SampleVariance(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The sample variance of the source</returns>
        public static double SampleVariance<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).SampleVariance();
        }

        /// <summary>
        /// Finds the population variance of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>The population variance of the source</returns>
        public static double PopulationVariance(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            double mean = Mean(src);
            var sum = 0.0;
            for (var i = 0; i < len; i++)
            {
                sum += Math.Pow((src.ElementAt(i) - mean), 2);
            }
            sum /= len;

            return sum;
        }

        /// <summary>
        /// A generic overload of the <see cref="PopulationVariance(IEnumerable{double})"/> method. This method will call
        /// the <see cref="PopulationVariance(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The populationvariance of the source</returns>
        public static double PopulationVariance<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).PopulationVariance();
        }

        /// <summary>
        /// Finds the sample standard deviation of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>The sample standard deviation of the source</returns>
        public static double SampleStandardDeviation(this IEnumerable<double> src)
        {
            src = src ?? throw new ArgumentNullException(nameof(src));
            if (!src.Any()) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            double variance = SampleVariance(src);
            return Math.Round(variance, 2);
        }

        /// <summary>
        /// A generic overload of the <see cref="SampleStandardDeviation(IEnumerable{double})"/> method. This method will call
        /// the <see cref="SampleStandardDeviation(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The sample standard deviation of the source</returns>
        public static double SampleStandardDeviation<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).SampleStandardDeviation();
        }

        /// <summary>
        /// Finds the population standard deviation of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>The population standard deviation of the source</returns>
        public static double PopulationStandardDeviation(this IEnumerable<double> src)
        {
            src = src ?? throw new ArgumentNullException(nameof(src));
            if (!src.Any()) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            double variance = PopulationVariance(src);
            return Math.Round(variance, 2);
        }

        /// <summary>
        /// A generic overload of the <see cref="PopulationStandardDeviation(IEnumerable{double})"/> method.
        /// This method will call the <see cref="PopulationStandardDeviation(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The population standard deviation of the source</returns>
        public static double PopulationStandardDeviation<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).PopulationStandardDeviation();
        }

        /// <summary>
        /// Finds the range of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of size 0</exception>
        /// <returns>The range of the source</returns>
        public static double Range(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            var src0 = src.ElementAt(0);

            if (len == 1)
            {
                return src0;
            }

            return src.ElementAt(len - 1) - src0;
        }

        /// <summary>
        /// A generic overload of the <see cref="Range(IEnumerable{double})"/> method. This method will call
        /// the <see cref="Range(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The range of the source</returns>
        public static double Range<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).Range();
        }

        /// <summary>
        /// Finds the lower quartile of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumberable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of size 0</exception>
        /// <exception cref="InsufficientDataSetException">Thrown when the data set is not large enough to calculate a lower quartile</exception>
        /// <returns>The lower quartile of the source</returns>
        public static double LowerQuartile(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            switch (len)
            {
                case 1:
                case 2:
                    throw new InsufficientDataSetException("Data set not large enough to calculate an lower quartile");
                case 0:
                    throw new InvalidOperationException(nameof(src));
            }
            Contract.EndContractBlock();
            var lowerHalf = new int[len / 2];

            return lowerHalf.Median(num => num);
        }

        /// <summary>
        /// A generic overload of the <see cref="LowerQuartile(IEnumerable{double})"/> method. This method will call
        /// the <see cref="LowerQuartile(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The lower quartile of the source</returns>
        public static double LowerQuartile<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).LowerQuartile();
        }

        /// <summary>
        /// Finds the upper quartile of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of size 0</exception>
        /// <exception cref="InsufficientDataSetException">Thrown when the data set is not large enough to calculate an upper quartile</exception>
        /// <returns>The upper quartile of the source</returns>
        public static double UpperQuartile(this IEnumerable<double> src)
        {
            int len = src.Count();

            src = src ?? throw new ArgumentNullException(nameof(src));
            switch (len)
            {
                case 1:
                case 2:
                    throw new InsufficientDataSetException("Data set not large enough to calculate an upper quartile");
                case 0:
                    throw new InvalidOperationException(nameof(src));
            }
            Contract.EndContractBlock();
            var upperHalf = new int[len / 2];
            return upperHalf.Median(num => num);
        }

        /// <summary>
        /// A generic overload of the <see cref="UpperQuartile(IEnumerable{double})"/> method. This method will call
        /// the <see cref="UpperQuartile(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The upper quartile of the source</returns>
        public static double UpperQuartile<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).UpperQuartile();
        }

        /// <summary>
        /// Finds the inter-quartile range of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>The inter-quartile range of the source</returns>
        public static double InterQuartileRange(this IEnumerable<double> src)
        {

            src = src ?? throw new ArgumentNullException(nameof(src));
            if (!src.Any()) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            double lowerQuart = LowerQuartile(src);
            double upperQuart = UpperQuartile(src);
            return upperQuart - lowerQuart;
        }

        /// <summary>
        /// A generic overload of the <see cref="InterQuartileRange(IEnumerable{double})"/> method. This method will call
        /// the <see cref="InterQuartileRange(IEnumerable{double})(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>The inter-quartile range of the source</returns>
        public static double InterQuartileRange<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).InterQuartileRange();
        }

        /// <summary>
        /// Returns whether if the data set is normally distributed for a proportion.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <param name="samstat">The sample statistic</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>Returns if the data set is normally distributed for a proportion.</returns>
        public static bool IsNormalProportion(this IEnumerable<double> src, double samstat)
        {
            int len = src.Count();
            _ = src ?? throw new ArgumentNullException(nameof(src));
            if (len == 0) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            if ((len * samstat) <= 15) return false;
            return (len * (1 - samstat)) <= 15 ? false : true;
        }

        /// <summary>
        /// A generic overload of the <see cref="IsNormalProportion(IEnumerable{double})"/> method. This method will call
        /// the <see cref="IsNormalProportion(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <param name="samstat">The sample statistic></param>
        /// <returns>Returns if the data set is normally distributed for a proportion.</returns>
        public static bool IsNormalProportion<T>(this IEnumerable<T> numbers, Func<T, double> selector, double samstat)
        {
            return (from num in numbers select selector(num)).IsNormalProportion(samstat);
        }

        /// <summary>
        /// Returns whether if the data set is normally distributed for a mean.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source is of length 0</exception>
        /// <returns>Returns whether if the data set is normally distributed for a mean.</returns>
        public static bool IsNormalMean(this IEnumerable<double> src)
        {
            src = src ?? throw new ArgumentNullException(nameof(src));
            if (!src.Any()) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            return !(Math.Abs(Mode(src)) > TOLERANCE);
        }

        /// <summary>
        /// A generic overload of the <see cref="IsNormalMean(IEnumerable{double})"/> method. This method will call
        /// the <see cref="IsNormalMean(IEnumerable{double})"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returns>Returns if the data set is normally distributed for a proportion.</returns>
        public static bool IsNormalMean<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).IsNormalMean();
        }

        /// <summary>
        /// Calculates the standardized score (z-score or standard score) of the enumerable.
        /// </summary>
        /// <param name="src">The IEnumerable of type double</param>
        /// <param name="elem">The value of the element</param>
        /// <exception cref="ArgumentNullException">Thrown when the source is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the source if of length 0</exception>
        /// <returns>The standardized score of the source</returns>
        public static double StandardizedScore(this IEnumerable<double> src, double elem)
        {
            src = src ?? throw new ArgumentNullException(nameof(src));
            if (!src.Any()) throw new InvalidOperationException(nameof(src));
            Contract.EndContractBlock();

            return (elem - Mean(src)) / PopulationStandardDeviation(src);
        }

        /// <summary>
        /// A generic overload of the <see cref="StandardizedScore(IEnumerable{double}, double)"/> method. 
        /// This method will call the <see cref="StandardizedScore(IEnumerable{double}, double)"/> overload.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="numbers">The specified enumerable</param>
        /// <param name="selector">The numeral specifier</param>
        /// <returnsThe standardized score of the source</returns>
        public static double StandardizedScore<T>(this IEnumerable<T> numbers, Func<T, double> selector, double elem)
        {
            return (from num in numbers select selector(num)).StandardizedScore(elem);
        }

        /// <summary>
        /// Creates confidence interval for the given data set.
        /// </summary>
        /// <param name="mean">The mean of the data set</param>
        /// <param name="cv">The critical value of the data set</param>
        /// <param name="se">The standard error of the data set</param>
        /// <returns>Returns a confidence interval of the data set.</returns>
        public static IEnumerable<double> CreateConfidenceInterval(double mean, double cv, double se)
        {
            var ci = new double[2];
            var lowBound = mean - (cv * se);
            var upperBound = mean + (cv * se);
            ci[0] = lowBound;
            ci[1] = upperBound;
            return ci;
        }

        /// <summary>
        /// Returns whether if the data set is normally distributed for a mean.
        /// </summary>
        /// <param name="mean">The mean of the data set</param>
        /// <param name="popMean">The population mean</param>
        /// <param name="stdDev">The standard deviation of the data set</param>
        /// <param name="size">The size of the data set</param>
        /// <returns>Constructs the t-critical value</returns>
        public static double ConstructTValue(double mean, double popMean, double stdDev, double size)
        {
            return (mean - popMean) / (stdDev / Math.Sqrt(size));
        }

    }//EnumerableStats
}//namespace Utilities
