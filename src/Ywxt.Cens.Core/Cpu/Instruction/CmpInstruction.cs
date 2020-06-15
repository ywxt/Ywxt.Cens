using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xC9, AddressingMode.ImmediateAddressingMode},
                {0xC5, AddressingMode.ZeroPageAddressingMode},
                {0xD5, AddressingMode.ZeroPageXAddressingMode},
                {0xCD, AddressingMode.AbsoluteAddressingMode},
                {0xDD, AddressingMode.AbsoluteXAddressingMode},
                {0xD9, AddressingMode.AbsoluteYAddressingMode},
                {0xC1, AddressingMode.IndirectXAddressingMode},
                {0xD2, AddressingMode.IndirectYAddressingMode}
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
                0xC5 => 3,
                0xD5 => 4,
                0xCD => 4,
                0xDD => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0xD9 => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0xC1 => 6,
                0xD2 => 5 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}