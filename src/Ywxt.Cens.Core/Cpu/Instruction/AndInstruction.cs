using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AndInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x29, AddressingMode.ImmediateAddressingMode},
                {0x25, AddressingMode.ZeroPageAddressingMode},
                {0x35, AddressingMode.ZeroPageXAddressingMode},
                {0x2D, AddressingMode.AbsoluteAddressingMode},
                {0x3D, AddressingMode.AbsoluteXAddressingMode},
                {0x39, AddressingMode.AbsoluteYAddressingMode},
                {0x21, AddressingMode.IndirectXAddressingMode},
                {0x31, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = (byte) (cpu.Registers.A & data);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x29 => 2,
                0x25 => 3,
                0x35 => 4,
                0x2D => 4,
                0x3D => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0x39 => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0x21 => 6,
                0x31 => 5 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}