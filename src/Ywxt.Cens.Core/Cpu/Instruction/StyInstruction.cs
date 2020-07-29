using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x84, AddressingMode.ZeroPageAddressingMode},
                {0x94, AddressingMode.ZeroPageXAddressingMode},
                {0x8C, AddressingMode.AbsoluteAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.Y);
            return instruction switch
            {
                0x84 => 3,
                0x94 => 4,
                0x8C => 4,
                _ => 0
            };
        }
    }
}