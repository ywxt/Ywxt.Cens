using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x86, (AddressingType.Address, AddressingMode.ZeroPageAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Bus.WriteByte(data, cpu.Registers.X);
            return instruction switch
            {
                0x86 => 3,
                _ => 0
            };
        }
    }
}