using System;
using Ywxt.Cens.Core.Cpu;

namespace Ywxt.Cens.Core.Exceptions
{
    public class UnknownAddressingModeException : Exception
    {
        public UnknownAddressingModeException(AddressingMode addrMode) : base($"未知的寻址模式: {addrMode}")
        {
        }
    }
}