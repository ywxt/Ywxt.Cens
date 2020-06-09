using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0xA0, (AddressingType.Data, AddressingMode.ImmediateAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
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