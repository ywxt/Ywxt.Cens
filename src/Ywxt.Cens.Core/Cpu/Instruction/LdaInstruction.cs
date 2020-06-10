using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA9,  AddressingMode.ImmediateAddressingMode},
                {0xA5, AddressingMode.ImmediateAddressingMode},
            };

        public AddressingType AddressingType { get; }= AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            cpu.Registers.A = (byte) data;
            cpu.Registers.SetZAndN(cpu.Registers.A);

            return instruction switch
            {
                0xA9 => 2,
                _ => 0
            };
        }
    }
}