using System;
using System.Collections.Generic;
using System.Text;

namespace DevDragon.AV.Client
{
    public class FileScanException : Exception
    {
        public FileScanException() { }
        public FileScanException(string message) : base(message) { }
        public FileScanException(string message, Exception inner) : base(message, inner) { }
        protected FileScanException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
