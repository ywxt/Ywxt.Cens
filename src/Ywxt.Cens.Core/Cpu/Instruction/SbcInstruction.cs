using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SbcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xE9, AddressingMode.ImmediateAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var result = unchecked(cpu.Registers.A - data - 1 + (byte) (cpu.Registers.P & PFlags.C));
            var af = cpu.Registers.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            //判断溢出
            if ((af == 1 && cf == 0) | (af == 0 && bf == 1 && cf == 1))
            {
                cpu.Registers.P |= PFlags.V;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.V;
            }

            if (((result >> 8) & 1) != 1)
            {
                cpu.Registers.P |= PFlags.C;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.C;
            }

            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0xE9 => 2,
                _ => 0
            };
        }
    }
}