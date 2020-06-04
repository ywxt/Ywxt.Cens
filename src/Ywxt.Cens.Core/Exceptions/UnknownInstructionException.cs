using System;

namespace Ywxt.Cens.Core.Exceptions
{
    public class UnknownInstructionException : Exception
    {
        public UnknownInstructionException(byte instruction) : base($"未知的指令: {instruction:X}")
        {
        }
    }
}