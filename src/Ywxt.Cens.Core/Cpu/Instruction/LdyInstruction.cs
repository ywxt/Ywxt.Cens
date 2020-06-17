using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA0, AddressingMode.ImmediateAddressingMode},
                {0xA4, AddressingMode.ZeroPageAddressingMode},
                {0xB4, AddressingMode.ZeroPageXAddressingMode},
                {0xAC, AddressingMode.AbsoluteAddressingMode},
                {0xBC, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data)
        {
            cpu.Registers.Y = data;
            cpu.Registers.SetZAndN(cpu.Registers.Y);

            return instruction switch
            {
                0xA0 => 2,
                0xA4 => 3,
                0xB4 => 4,
                0xAC => 4,
                0xBC => 4 + InstructionUtil.GetClockCycleByCrossingPage(cpu.Registers.Pc, address),
                _ => 0
            };
        }
    }
}