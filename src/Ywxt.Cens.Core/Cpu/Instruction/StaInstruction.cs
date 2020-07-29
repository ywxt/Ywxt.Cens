using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x85, AddressingMode.ZeroPageAddressingMode},
                {0x95, AddressingMode.ZeroPageXAddressingMode},
                {0x8D, AddressingMode.AbsoluteAddressingMode},
                {0x9D, AddressingMode.AbsoluteXAddressingMode},
                {0x99, AddressingMode.AbsoluteYAddressingMode},
                {0x81, AddressingMode.IndirectXAddressingMode},
                {0x91, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.A);
            return instruction switch
            {
                0x85 => 3,
                0x95 => 4,
                0x8D => 4,
                0x9D => 5,
                0x99 => 5,
                0x81 => 6,
                0x91 => 6,
                _ => 0
            };
        }
    }
}