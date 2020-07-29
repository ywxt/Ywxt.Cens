using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CpxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xE0, AddressingMode.ImmediateAddressingMode},
                {0xE4, AddressingMode.ZeroPageAddressingMode},
                {0xEC, AddressingMode.AbsoluteAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.GetData(address, cpu, instruction);
            var result = cpu.Registers.X - data;
            cpu.Registers.SetZAndN(unchecked((byte) result));
            cpu.Registers.SetC(result >= 0);

            return instruction switch
            {
                0xE0 => 2,
                0xE4 => 3,
                0xEC => 4,
                _ => 0
            };
        }
    }
}