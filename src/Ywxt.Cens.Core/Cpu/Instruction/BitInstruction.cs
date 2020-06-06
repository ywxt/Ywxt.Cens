using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BitInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x24, (AddressingType.Address, AddressingMode.ZeroPageAddressingMode)},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var value = cpu.Bus.ReadByte(data);
            if ((value & 0b10000000) >> 7 == 1)
            {
                cpu.Registers.P |= PFlags.N;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.N;
            }

            if ((value& 0b01000000) >> 6 == 1)
            {
                cpu.Registers.P |= PFlags.V;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.V;
            }

            value &= cpu.Registers.A;
            if (value == 0)
            {
                cpu.Registers.P |= PFlags.Z;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.Z;
            }

            return instruction switch
            {
                0x24 => 3,
                _ => 0
            };
        }
    }
}