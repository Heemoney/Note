﻿using System;
using Note.Attributes;
namespace Note.UtilExceptions.EnumberableStatsExceptions
{
    [Author("Manu Puduvalli")]
    [Author("Sam Yuen")]
    [Serializable()]
    public class InsufficientDataSetException : Exception
    {
        public InsufficientDataSetException() : base() { }
        public InsufficientDataSetException(string message) : base(message) { }
        public InsufficientDataSetException(string message, System.Exception inner) : base(message, inner) { }

        protected InsufficientDataSetException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Author("Manu Puduvalli")]
    [Author("Sam Yuen")]
    [Serializable()]
    public class NoModeException : Exception
    {
        public NoModeException() : base() { }
        public NoModeException(string message) : base(message) { }
        public NoModeException(string message, System.Exception inner) : base(message, inner) { }

        protected NoModeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

