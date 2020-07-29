using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x28, AddressingMode.ImplicitAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var flags = cpu.Stack.PopByte();
            cpu.Registers.P = (PFlags) flags;
            cpu.Registers.P &= ~PFlags.B;
            cpu.Registers.P |= PFlags.U;
            return instruction switch
            {
                0x28 => 4,
                _ => 0
            };
        }
    }
}