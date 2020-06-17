using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class ClcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x18, AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
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