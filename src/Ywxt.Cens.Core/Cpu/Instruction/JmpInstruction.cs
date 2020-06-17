using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x4C, AddressingMode.AbsoluteAddressingMode},
                {0x6C, AddressingMode.IndirectAddressingMode},
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data)
        {
            cpu.Registers.Pc = address;
            return instruction switch
            {
                0x4C => 3,
                0x6C => 5,
                _ => 0,
            };
        }
    }
}