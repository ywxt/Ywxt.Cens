using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x4C, (AddressingType.Address, AddressingMode.AbsoluteAddressingMode)},
                {0x6C, (AddressingType.Address, AddressingMode.IndirectAddressingMode)},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.Pc = data;
            return instruction switch
            {
                0x4C => 3,
                0x6C => 5,
                _ => 0,
            };
        }
    }
}