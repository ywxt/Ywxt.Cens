using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class ClcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x18, (AddressingType.Address, AddressingMode.ImplicitAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.P &= ~PFlags.C;
            return instruction switch
            {
                0x18 => 2,
                _ => 0,
            };
        }
    }
}