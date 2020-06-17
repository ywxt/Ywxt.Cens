using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TxaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x8A, AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = cpu.Registers.X;
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x8A => 2,
                _ => 0
            };
        }
    }
}