using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SeiInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
         = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
         {
             {0x78,(AddressingType.Data,AddressingMode.ImplicitAddressingMode)}
         };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.P |= PFlags.I;
            return instruction switch
            {
                0x78 => 2,
                _ => 0
            };
        }
    }
}