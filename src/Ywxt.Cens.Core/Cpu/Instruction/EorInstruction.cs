using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class EorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x49, AddressingMode.ImmediateAddressingMode},
                {0x45, AddressingMode.ZeroPageAddressingMode},
                {0x55, AddressingMode.ZeroPageXAddressingMode},
                {0x4D, AddressingMode.AbsoluteAddressingMode},
                {0x5D, AddressingMode.AbsoluteXAddressingMode},
                {0x59, AddressingMode.AbsoluteYAddressingMode},
                {0x41, AddressingMode.IndirectXAddressingMode},
                {0x52, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = (byte) (cpu.Registers.A ^ data);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x49 => 2,
                0x45 => 3,
                0x55 => 4,
                0x4D => 4,
                0x5D => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0x59 => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0x41 => 6,
                0x52 => 5 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}