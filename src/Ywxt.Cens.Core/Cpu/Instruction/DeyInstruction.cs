using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DeyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x88, AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.Y--;
            cpu.Registers.SetZAndN(cpu.Registers.Y);
            return instruction switch
            {
                0x88 => 2,
                _ => 0
            };
        }
    }
}