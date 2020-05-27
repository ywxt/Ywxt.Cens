using System;
using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu
{
    public interface IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }

        /// <summary>
        /// 返回所需时钟周期
        /// </summary>
        /// <param name="cpu"></param>
        /// <param name="instruction"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Invoke(ICpu cpu, byte instruction, ushort data);
    }
}