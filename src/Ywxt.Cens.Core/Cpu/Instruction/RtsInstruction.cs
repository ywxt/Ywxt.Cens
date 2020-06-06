using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RtsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x60, (AddressingType.Data, AddressingMode.ImplicitAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var address = cpu.Stack.PopWord();
            cpu.Registers.Pc = address;

            return instruction switch
            {
                0x60 => 6,
                _ => 0
            };
        }
    }
}