using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA9, AddressingMode.ImmediateAddressingMode},
                {0xA5, AddressingMode.ZeroPageAddressingMode},
                {0xB5, AddressingMode.ZeroPageXAddressingMode},
                {0xAD, AddressingMode.AbsoluteAddressingMode},
                {0xBD, AddressingMode.AbsoluteXAddressingMode},
                {0xB9, AddressingMode.AbsoluteYAddressingMode},
                {0xA1, AddressingMode.IndirectXAddressingMode},
                {0xB1, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = data;
            cpu.Registers.SetZAndN(cpu.Registers.A);

            return instruction switch
            {
                0xA9 => 2,
                0xA5 => 3,
                0xB5 => 4,
                0xAD => 4,
                0xBD => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0xB9 => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0xA1 => 6,
                0xB1 => 5 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}