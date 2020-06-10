using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA0,  AddressingMode.ImmediateAddressingMode}
            };

        public AddressingType AddressingType { get; }= AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            cpu.Registers.Y = (byte) data;
            cpu.Registers.SetZAndN(cpu.Registers.Y);

            return instruction switch
            {
                0xA0 => 2,
                _ => 0
            };
        }
    }
}