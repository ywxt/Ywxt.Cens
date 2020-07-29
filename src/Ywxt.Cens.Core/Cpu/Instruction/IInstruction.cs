using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public interface IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }

        /// <summary>
        /// 返回所需时钟周期
        /// </summary>
        /// <param name="cpu">CPU</param>
        /// <param name="instruction">指令字节</param>
        /// <param name="address">寻址结果</param>
        /// <param name="pageCrossed">是否跨页</param>
        /// <returns></returns>
        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed);
    }
}