using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0xA9, (AddressingType.Data, AddressingMode.ImmediateAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.A = (byte) data;
            if (cpu.Registers.A == 0)
            {
                cpu.Registers.P |= PFlags.Z;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.Z;
            }

            if (cpu.Registers.A >> 7 == 1)
            {
                cpu.Registers.P |= PFlags.N;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.N;
            }

            return instruction switch
            {
                0xA9 => 2,
                _ => 0
            };
        }
    }
}