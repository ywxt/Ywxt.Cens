using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class NopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x1A, AddressingMode.ImplicitAddressingMode},
                {0x3A, AddressingMode.ImplicitAddressingMode},
                {0x5A, AddressingMode.ImplicitAddressingMode},
                {0x7A, AddressingMode.ImplicitAddressingMode},
                {0xDA, AddressingMode.ImplicitAddressingMode},
                {0xEA, AddressingMode.ImplicitAddressingMode},
                {0xFA, AddressingMode.ImplicitAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            return instruction switch
            {
                0x1A => 2,
                0x3A => 2,
                0x5A => 2,
                0x7A => 2,
                0xDA => 2,
                0xEA => 2,
                0xFA => 2,
                _ => 0,
            };
        }
    }
}