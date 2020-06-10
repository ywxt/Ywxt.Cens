using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AndInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode > OpCodes { get; }
        = new Dictionary<byte, AddressingMode>
        {
            {0x29,AddressingMode.ImmediateAddressingMode}
        };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            cpu.Registers.A = (byte) (cpu.Registers.A & data);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x29 => 2,
                _ => 0
            };
        }
    }
}