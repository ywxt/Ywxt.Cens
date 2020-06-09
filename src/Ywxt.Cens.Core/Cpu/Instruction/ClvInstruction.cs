using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class ClvInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
         = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
         {
             {0xB8,(AddressingType.Data,AddressingMode.ImplicitAddressingMode)}
         };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.P &= ~PFlags.V;
            return instruction switch
            {
                0xB8 => 2,
                _ => 0
            };
        }
    }
}