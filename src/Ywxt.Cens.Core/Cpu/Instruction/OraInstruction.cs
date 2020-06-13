using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class OraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x09, AddressingMode.ImmediateAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = (byte) (cpu.Registers.A | data);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x09 => 2,
                _ => 0
            };
        }
    }
}