using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class OraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x09, AddressingMode.ImmediateAddressingMode},
                {0x05, AddressingMode.ZeroPageAddressingMode},
                {0x15, AddressingMode.ZeroPageXAddressingMode},
                {0x0D, AddressingMode.AbsoluteAddressingMode},
                {0x1D, AddressingMode.AbsoluteXAddressingMode},
                {0x19, AddressingMode.AbsoluteYAddressingMode},
                {0x01, AddressingMode.IndirectXAddressingMode},
                {0x11, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data)
        {
            cpu.Registers.A = (byte) (cpu.Registers.A | data);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x09 => 2,
                0x05 => 3,
                0x15 => 4,
                0x0D => 4,
                0x1D => 4 + InstructionUtil.GetClockCycleByCrossingPage(cpu.Registers.Pc, address),
                0x19 => 4 + InstructionUtil.GetClockCycleByCrossingPage(cpu.Registers.Pc, address),
                0x01 => 6,
                0x11 => 5 + InstructionUtil.GetClockCycleByCrossingPage(cpu.Registers.Pc, address),
                _ => 0
            };
        }
    }
}