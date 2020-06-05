using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class NopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0xEA, (AddressingType.Address, AddressingMode.ImplicitAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            return instruction switch
            {
                0xEA => 2,
                _ => 0,
            };
        }
    }
}