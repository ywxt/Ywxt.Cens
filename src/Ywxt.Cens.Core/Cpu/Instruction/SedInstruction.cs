using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SedInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
         = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
         {
             {0xF8,(AddressingType.Data,AddressingMode.ImplicitAddressingMode)}
         };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.P |= PFlags.D;
            return instruction switch
            {
                0xF8 => 2,
                _ => 0
            };
        }
    }
}