using System;

namespace Exceptions
{
    public class InvalidKeySizeException : Exception
    {
        string Message
        {
            get;
        }
        public InvalidKeySizeException()
        {
            this.Message = "Message key size only valid for 128, 192 or 256";
        }
    }
}
