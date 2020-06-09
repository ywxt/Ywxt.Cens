using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
         = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
         {
             {0x28,(AddressingType.Data,AddressingMode.ImplicitAddressingMode)}
         };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var flags = cpu.Stack.PopByte();
            cpu.Registers.P = (PFlags) flags;
            cpu.Registers.P &= ~PFlags.B;
            cpu.Registers.P |= PFlags.U;
            return instruction switch
            {
                0x28 => 4,
                _ => 0
            };
        }
    }
}