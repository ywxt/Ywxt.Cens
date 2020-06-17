using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RtiInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x40, AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var flags = (byte) (cpu.Stack.PopByte() | 0b0010_0000);
            cpu.Registers.P = (PFlags) flags;
            cpu.Registers.Pc = cpu.Stack.PopWord();
            return instruction switch
            {
                0x40 => 6,
                _ => 0
            };
        }
    }
}