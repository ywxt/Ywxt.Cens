using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xC9, AddressingMode.ImmediateAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var result = cpu.Registers.A - data;
            cpu.Registers.SetZAndN(unchecked((byte) result));
            if (result >= 0)
            {
                cpu.Registers.P |= PFlags.C;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.C;
            }

            return instruction switch
            {
                0xC9 => 2,
                _ => 0
            };
        }
    }
}