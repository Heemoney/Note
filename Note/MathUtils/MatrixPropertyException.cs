using System;

namespace Note.UtilExceptions.MatrixExceptions
{
    /// <summary>
    /// An exception that occurs if a Matrix property is violated
    /// when examining certain properties at runtime.
    /// </summary>
    [Attributes.Author("Manu Puduvalli")]
    [Serializable()]
    public class MatrixPropertyException : Exception
    {
        public MatrixPropertyException() : base() { }
        public MatrixPropertyException(string message) : base(message) { }
        public MatrixPropertyException(string message, System.Exception inner) : base(message, inner) { }

        protected MatrixPropertyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
