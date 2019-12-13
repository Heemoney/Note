using Note.UtilExceptions.MatrixExceptions;
using Note.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Note.MathUtils
{
    /// <summary>
    /// A class representing a mathematical Matrix. Creates a rectangular
    /// array of rows and columns with numbers as elements. The Matrix
    /// class includes mathematical matrix operations to manipulate it.
    /// </summary>
    [Author("Manu Puduvalli")]
    [Beta]
    public class Matrix : IMatrixer<double>, Note.Common.Base.IIndexableDouble<int, double>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Multidimensional does not waste space")]
        private double[,] Values { get; set; }

        /*
         * Use the auto property here
         */
        public int Rows { get; }
        public int Cols { get; }

        /// <summary>
        /// Creates an instance of a Matrix given rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows in this Matrix</param>
        /// <param name="cols">The number of columns in this Matrix</param>
        public Matrix(int rows, int cols)
        {
            Values = new double[rows, cols];
            Rows = rows;
            Cols = cols;
        }

        /// <summary>
        /// Creates an instance of a Matrix given a 2D array. 
        /// </summary>
        /// <param name="matrix">A 2D array of doubles</param>
        public Matrix(double[,] matrix)
        {
            matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            Values = matrix;
            Rows = matrix.GetLength(0);
            Cols = matrix.GetLength(1);
        }

        public double this[int row, int col]
        {
            get => Values[row, col];
  
            set => Values[row, col] = value;
        }

        public Matrix Transpose()
        {
            var cp = new double[Cols, Rows];
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    cp[c, r] = Values[r, c];
                }
            }
            Values = cp;
            return this;
        }

        private protected static Matrix DoScalar(Matrix m, double? sc, Func<double, double?, double> action)
        {
            for (var r = 0; r < m.Rows; r++)
            {
                for (var c = 0; c < m.Cols; c++)
                {
                    m[r, c] = action(m[r, c], sc);
                }
            }
            return m;
        }

        public static Matrix operator +(Matrix m, double scalar) => DoScalar(m, scalar, (val, sc) => val + (double)sc);

        public static Matrix operator +(double scalar, Matrix m) => DoScalar(m, scalar, (val, sc) => val + (double)sc);

        public static Matrix operator -(Matrix m, double scalar) => DoScalar(m, scalar, (val, sc) => val - (double)sc);

        public static Matrix operator -(double scalar, Matrix m) => DoScalar(m, scalar, (val, sc) => val - (double)sc);

        public static Matrix operator *(Matrix m, double scalar) => DoScalar(m, scalar, (val, sc) => val * (double)sc);

        public static Matrix operator *(double scalar, Matrix m) => DoScalar(m, scalar, (val, sc) => val * (double)sc);

        public static Matrix operator /(Matrix m, double scalar) => DoScalar(m, scalar, (val, sc) => val / (double)sc);

        public static Matrix operator /(double scalar, Matrix m) => DoScalar(m, scalar, (sc, val) => sc / (double)val);

        public static Matrix operator %(Matrix m, double scalar) => DoScalar(m, scalar, (val, sc) => val % (double)sc);

        public static Matrix operator %(double scalar, Matrix m) => DoScalar(m, scalar, (sc, val) => sc % (double)val);

        public static Matrix operator !(Matrix m) => DoScalar(m, null, (val, sc) => val - (val * 2));

        public static Matrix operator *(Matrix one, Matrix two)
        {
            one = one ?? throw new ArgumentNullException(nameof(one));
            two = two ?? throw new ArgumentNullException(nameof(two));

            Matrix cp = new Matrix(one.Rows, two.Cols);

            if(one.Cols != two.Rows)
            {
                throw new MatrixPropertyException($"Found: Matrix one columns: {one.Cols}, Matrix Two rows: {one.Cols} "
                                                  + "but required Matrix one columns == Matrix two rows");
            }

            for (var i = 0; i < one.Rows; i++)
            {
                for (var j = 0; j < two.Cols; j++)
                {
                    for (var k = 0; k < one.Cols; k++)
                    {
                        cp[i, j] += one[i, k] * two[k, j];
                    }
                }
            }
            return cp;
        }

        public static bool operator ==(Matrix one, Matrix two)
        {
            if(one is null || two is null)
            {
                return false;
            }
            if (!EqualDimension(one, two))
                return false;

            for(var i = 0; i < one.Rows; i++)
            {
                for(var j = 0; j < one.Cols; j++)
                {
                    if (one[i, j] != two[i, j]) 
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix one, Matrix two)
        {
            if (one is null || two is null)
            {
                return true;
            }
            if (!EqualDimension(one, two))
                return true;

            for (var i = 0; i < one.Rows; i++)
            {
                for (var j = 0; j < one.Cols; j++)
                {
                    if (one[i, j] != two[i, j])
                        return true;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            var sec = obj as Matrix;
            return this == sec;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool EqualDimension(Matrix one, Matrix two)
        {
            one = one ?? throw new ArgumentNullException(nameof(one));
            two = two ?? throw new ArgumentNullException(nameof(two));
            return one.Rows == two.Rows && one.Cols == two.Cols;
        }

        public bool IsRowVector() => Rows == 1;

        public bool IsColumnVector() => Cols == 1;

        public bool IsSquareVector() => Rows == Cols;

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    sb.Append($"{Values[i, j]} \t");
                }
                sb.Append(Environment.NewLine + Environment.NewLine);
            }
            return sb.ToString();
        }

        public IEnumerator<double> GetEnumerator()
        {
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    yield return Values[Rows, Cols];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Matrix Add(Matrix left, double scalar)
           => left + scalar;

        public static Matrix Subtract(Matrix left, double scalar)
            => left + scalar;

        public static Matrix Multiply(Matrix left, double scalar)
            => left * scalar;

        public static Matrix DotProduct(Matrix left, Matrix right)
            => left * right;

        public static Matrix Divide(Matrix left, double scalar)
            => left / scalar;

        public static Matrix Mod(Matrix left, double scalar)
            => left % scalar;

        public static Matrix Negate(Matrix m)
            => !m;
    }
}