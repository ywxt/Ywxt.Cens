using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public interface IInstruction
    {
        public IReadOnlyDictionary<byte,  AddressingMode> OpCodes { get; }
        public AddressingType AddressingType { get; }

        /// <summary>
        /// 返回所需时钟周期
        /// </summary>
        /// <param name="cpu"></param>
        /// <param name="instruction"></param>
        /// <param name="data"></param>
        /// <param name="pageCrossed"></param>
        /// <returns></returns>
        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed);
    }
}