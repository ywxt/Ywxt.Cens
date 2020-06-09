using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CpyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
         = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
         {
             {0xC0,(AddressingType.Data,AddressingMode.ImmediateAddressingMode)}
         };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var result = cpu.Registers.Y - (byte) data;
            cpu.Registers.SetZAndN(unchecked((byte) result));
            if (result >= 0)
            {
                cpu.Registers.P |= PFlags.C;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.C;
            }

            return instruction switch
            {
                0xC0 => 2,
                _ => 0
            };
        }
    }
}